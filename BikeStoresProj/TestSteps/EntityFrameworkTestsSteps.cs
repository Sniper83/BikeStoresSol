using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechTalk.SpecFlow;
using BikeStoresProj.Constants;

namespace BikeStoresProj.TestSteps
{
    [Binding]
    public class EntityFrameworkTestsSteps
    {
        [When(@"I get elements of Entity framework")]
        public void WhenIUseTheGetMethodOfEntityFramework()
        {
            var listOfBrandsNames = new List<string>();
            
            foreach (var item in ScenarioContext.Current.Get<DbSet<Brands>>("Brands Table"))
            {
                listOfBrandsNames.Add(item.BrandName);
            }

            ScenarioContext.Current.Add("List Of Brands Names", listOfBrandsNames);
        }
        
        [When(@"I use the Add method of Entity framework")]
        public void WhenIUseTheAddMethodOfEntityFramework()
        {
            var brandsTable = ScenarioContext.Current.Get<DbSet<Brands>>("Brands Table");

            brandsTable.Add(new Brands { BrandName = Consts.NewBrandName });

            var dataBase = ScenarioContext.Current.Get<BikeStoresContext>("Bike Stores Database");

            dataBase.SaveChanges();
        }
        
        [When(@"I use the Remove method of Entity framework")]
        public void WhenIUseTheRemoveMethodOfEntityFramework()
        {
            var brandsTable = ScenarioContext.Current.Get<DbSet<Brands>>("Brands Table");

            foreach (var item in brandsTable)
            {
                if (item.BrandName == Consts.NewBrandName)
                {
                    brandsTable.Remove(item);
                }
            }

            var dataBase = ScenarioContext.Current.Get<BikeStoresContext>("Bike Stores Database");

            dataBase.SaveChanges();
        }
        
        [Then(@"I will receive a list of table elements")]
        public void ThenItWillReturnAListOfTableElements()
        {
            Assert.That(!ScenarioContext.Current.Get<List<string>>("List Of Brands Names").Contains(null));
        }

        [Then(@"Get method will return a new element")]
        public void ThenGetMethodWillReturnANewElement()
        {
            var brandsTable = ScenarioContext.Current.Get<DbSet<Brands>>("Brands Table");

            var isRecordExist = false;

            foreach (var item in brandsTable)
            {
                if (item.BrandName == Consts.NewBrandName)
                {
                    isRecordExist = true;
                    break;
                }
            }

            Assert.That(isRecordExist);
        }
        
        [Then(@"Get method will not find removed element")]
        public void ThenGetMethodWillNotFindRemovedElement()
        {
            var brandsTable = ScenarioContext.Current.Get<DbSet<Brands>>("Brands Table");

            var isRecordExist = false;

            foreach (var item in brandsTable)
            {
                if (item.BrandName == Consts.NewBrandName)
                {
                    isRecordExist = true;
                    break;
                }
            }

            Assert.That(!isRecordExist);
        }

        [Given(@"database BikeStores with table Brands")]
        public void GivenDatabaseBikeStoresWithTableBrands()
        {
            var dataBase = new BikeStoresContext();
            ScenarioContext.Current.Add("Bike Stores Database", dataBase);
            ScenarioContext.Current.Add("Brands Table", dataBase.Brands);
        }

    }
}
