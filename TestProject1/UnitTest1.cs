using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StepChallenge.Database;
using System;
using System.Linq;

namespace TestProject1
{
    public class Tests
    {
        const string DATABASE_NAME = "StepChallengeDB";

        DBContext Context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DBContext>()
               .UseInMemoryDatabase(databaseName: DATABASE_NAME)
               .Options;

            Context = new DBContext(options);

            Context.Add(new StepPoint() { MileStone = 1, TargetSteps = 5000, Points = 10, EffectiveDate = DateTime.Parse("2019-01-01") });
            Context.Add(new StepPoint() { MileStone = 1, TargetSteps = 5000, Points = 15, EffectiveDate = DateTime.Parse("2019-07-01") });
            Context.Add(new StepPoint() { MileStone = 1, TargetSteps = 5000, Points = 20, EffectiveDate = DateTime.Parse("2020-01-01") });

            Context.Add(new StepPoint() { MileStone = 2, TargetSteps = 10000, Points = 20, EffectiveDate = DateTime.Parse("2019-01-01") });
            Context.Add(new StepPoint() { MileStone = 2, TargetSteps = 10000, Points = 25, EffectiveDate = DateTime.Parse("2019-07-01") });
            Context.Add(new StepPoint() { MileStone = 2, TargetSteps = 10000, Points = 30, EffectiveDate = DateTime.Parse("2020-01-01") });

            Context.SaveChanges();
        }

        [Test]
        public void Test1()
        {
            computePoints("ron.zhong", DateTime.Now, 5000);
            computePoints("ron.zhong", DateTime.Now, 5000);

            var user = Context.UserStepPoints
                .Where(x => x.UserId.Equals("ron.zhong"))
                .FirstOrDefault();

            Assert.AreEqual(user.Points, 30);
        }


        void computePoints(string UserId, DateTime StepDate, int Steps)
        {
            var user = Context.UserStepPoints.Where(x => x.UserId.Equals(UserId) && x.StepDate.Equals(StepDate.Date)).FirstOrDefault();

            if (user is null)
            {
                var points = getEligiblePoints(StepDate, Steps);

                Context.Add(new UserStepPoint() { UserId = UserId, StepDate = StepDate.Date, Steps = Steps, Points = points });
            }
            else
            {
                var steps = user.Steps + Steps;

                user.Points = getEligiblePoints(StepDate, steps);
            }

            Context.SaveChanges();
        }

        int getEligiblePoints(DateTime stepDate, int steps)
        {
            var record = Context.StepPoints
                .Where(x => stepDate > x.EffectiveDate && steps >= x.TargetSteps)
                .OrderByDescending(x => x.MileStone)
                .OrderByDescending(x => x.EffectiveDate)
                .FirstOrDefault();

            return record?.Points ?? 0;
        }
    }
}