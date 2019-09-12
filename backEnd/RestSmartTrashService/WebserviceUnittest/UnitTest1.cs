using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSmartTrashService.Controllers;
using RestSmartTrashService.Model;

namespace WebserviceUnittest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WeightReaderTestDag()
        {
            // arrange
            IList<weight> weightListe = RestSmartTrashService.Controllers.weightController.GetWeightAsync(1).Result;
            
            // act
            int length = weightListe.Count;
            // assert

            Assert.AreEqual(length, 1);
            

        }
        
        [TestMethod]
        public void WeightReaderTestUge()
        {
            // arrange
            IList<weight> weightListe = RestSmartTrashService.Controllers.weightController.GetWeightAsync(2).Result;

            // act
            int length = weightListe.Count;
            // assert

            Assert.AreEqual(length, 7);


        }
        [TestMethod]
        public void WeightReaderTestMonth()
        {
            // arrange
            IList<weight> weightListe = RestSmartTrashService.Controllers.weightController.GetWeightAsync(3).Result;

            // act
            int length = weightListe.Count;
            // assert

            Assert.AreEqual(length, 30);


        }

        [TestMethod]
        public void WeightReaderTestYear()
        {
            // arrange
            IList<weight> weightListe = RestSmartTrashService.Controllers.weightController.GetWeightAsync(4).Result;

            // act
            int length = weightListe.Count;
            // assert

            Assert.AreEqual(length, 365);


        }

        [TestMethod]
        public void WeightReaderTestLastWeek()
        {
            // arrange
            IList<weight> lastWeek = RestSmartTrashService.Controllers.weightController.GetWeightAsync(5).Result;
            IList<weight> currentWeek = RestSmartTrashService.Controllers.weightController.GetWeightAsync(2).Result;
            
            // act
            weight dage1 = currentWeek[0];
            weight forDag1 = lastWeek[0];
            
            // assert
            Assert.AreEqual(dage1.id, (forDag1.id + 13));
           


        }
    }
}
