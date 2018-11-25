using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mvc_proboosting.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using mvc_proboosting.Controllers;
    using mvc_proboosting.Models;

    using Moq;

    [TestClass]
    public class CustomersControllerTest
    {
        //global variables needed
        private CustomersController controller;

        private Mock<ICustomersMock> mock;

        private List<Customer> customers;

        [TestInitialize]
        public void TestInitialize()
        {
            // this method runs automatically before each individual test

            // create a new mock data object to hold a fake list of Customers
            this.mock = new Mock<ICustomersMock>();

            // populate mock list
            this.customers = new List<Customer>
                                 {
                                         new Customer
                                         {
                                             Booster =
                                                 new Booster
                                                     {
                                                         Customers = this.customers,
                                                         BoosterId = 2, DateCreated = DateTime.Now,
                                                         Email = "frank@yahoo.com", IsAvailable = true,
                                                         LastLogon = null,
                                                         FirstName = "Frank",
                                                         LastName = "Smith"
                                                     }, BoosterId = 2, Email = "alan@gmail.com",
                                             FirstName = "Alan",
                                             LastName = "Brown",
                                             LastLogon = null,
                                             DateCreated = DateTime.Now,
                                             CustomerId = 2
                                         },
                                         
                                         new Customer
                                         {
                                             Booster =
                                             new Booster
                                             {
                                                 Customers = this.customers,
                                                 BoosterId = 1, DateCreated = DateTime.Now,
                                                 Email = "john@yahoo.com", IsAvailable = true,
                                                 LastLogon = null,
                                                 FirstName = "John",
                                                 LastName = "Doe"
                                             }, BoosterId = 1, Email = "jane@gmail.com",
                                         FirstName = "Jane",
                                         LastName = "Doe",
                                         LastLogon = null,
                                         DateCreated = DateTime.Now,
                                         CustomerId = 1
                                         },
                                         new Customer
                                         {
                                             Booster =
                                                 new Booster
                                                     {
                                                         Customers = this.customers,
                                                         BoosterId = 3, DateCreated = DateTime.Now,
                                                         Email = "david@yahoo.com", IsAvailable = true,
                                                         LastLogon = null,
                                                         FirstName = "David",
                                                         LastName = "Hunter"
                                                     }, BoosterId = 3, Email = "michael@gmail.com",
                                             FirstName = "Michael",
                                             LastName = "Snow",
                                             LastLogon = null,
                                             DateCreated = DateTime.Now,
                                             CustomerId = 3
                                         }
                                 };

            // put list into mock object and pass it to the customers controller
            this.mock.Setup(m => m.Customers).Returns(this.customers.AsQueryable());
            this.controller = new CustomersController(this.mock.Object);
        }

        #region Index

        [TestMethod]
        public void IndexLoadsView()
        {
            // act
            ViewResult result = this.controller.Index() as ViewResult;

            //assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsCustomers()
        {
            // act
            var result = (List<Customer>)((ViewResult)this.controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(this.customers, result);
        }

        #endregion

        #region Details
        // GET: Customers/Details
        [TestMethod]
        public void DetailsNoId()
        {
            // act
            var result = (ViewResult)this.controller.Details(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            ViewResult result = (ViewResult)this.controller.Details(6);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            // act
            ViewResult result = (ViewResult)this.controller.Details(1);

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsAlbum()
        {
            // act
            Customer result = (Customer)((ViewResult)this.controller.Details(3)).Model;

            // assert
            Assert.AreEqual(this.customers[2], result);
        }

        #endregion

        #region Create

        //GET: Customers/Create

        [TestMethod]
        public void CreateGetViewBagNotNull()
        {
            // act
            var result = this.controller.Create() as ViewResult;

            // assert
            Assert.IsNotNull(result.ViewBag.BoosterId);
        }

        [TestMethod]
        public void CreateGetReturnsValidPage()
        {
            // act
            var result = this.controller.Create() as ViewResult;

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }

        //POST: Customers/Create

        [TestMethod]
        public void CreatePostValidDateCreated()
        {
            // act
            var result = this.controller.Create(this.customers[0]) as ViewResult;

            // assert
            Assert.AreEqual(DateTime.Now.ToString("f"), this.customers[0].DateCreated.ToString("f"));
        }

        [TestMethod]
        public void CreatePostValidEmail()
        {
            // act
            var result = this.controller.Create(this.customers[0]) as ViewResult;

            // assert
            Assert.AreEqual("alan@gmail.com", this.customers[0].Email);
        }

        [TestMethod]
        public void CreatePostValidRedirection()
        {
            // act
            var result = this.controller.Create(this.customers[0]) as RedirectToRouteResult;

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreatePostBoosterViewBagNotNull()
        {
            //act
            controller.ModelState.AddModelError("some error name", "fake error description");
            var result = (ViewResult)this.controller.Create(this.customers[0]);

            //assert
            Assert.IsNotNull(result.ViewBag.BoosterId);
        }

        [TestMethod]
        public void CreatePostValidPageLoads()
        {
            //act
            controller.ModelState.AddModelError("some error name", "fake error description");
            var result = (ViewResult)this.controller.Create(this.customers[0]);

            //assert
            Assert.AreEqual("Create", result.ViewName);
        }

        #endregion

        #region Edit
        //GET: Customers/Edit

        [TestMethod]
        public void EditGetNoId()
        {
            // act
            var result = (ViewResult)this.controller.Edit((int?)null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditGetInvalidCustomerId()
        {
            // act
            var result = (ViewResult)this.controller.Edit(6);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditGetViewBagBoosterNotNull()
        {
            // act
            var result = (ViewResult)this.controller.Edit(1);

            // assert
            Assert.IsNotNull(result.ViewBag.BoosterId);
        }

        [TestMethod]
        public void EditGetValidCustomerObject()
        {
            // act 
            Customer result = (Customer)((ViewResult)this.controller.Edit(1)).Model;

            // assert 
            Assert.AreEqual(this.customers[1], result);
        }


        [TestMethod]
        public void EditGetReturnsValidPage()
        {
            // act
            var result = (ViewResult)this.controller.Edit(1);

            // assert
            Assert.IsNotNull("Index", result.ViewName);
        }

        //POST: Customers/Edit
        [TestMethod]
        public void EditPostEmailValid()
        {
            // arrange
            controller.ModelState.AddModelError("some error name", "Error description");
            var uppercaseEmail = this.customers[0].Email.ToUpper();
            var uppercasedEmailCustomerList = this.customers;
            uppercasedEmailCustomerList[0].Email = uppercaseEmail;

            // act 
            var result = (Customer)((ViewResult)controller.Edit(uppercasedEmailCustomerList[0])).Model;

            // assert
            Assert.AreEqual("alan@gmail.com", result.Email);
        }

        [TestMethod]
        public void EditPostRedirectToValidPage()
        {
            // act
            var result = this.controller.Edit(this.customers[0]) as RedirectToRouteResult;

            // arrange
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void EditPostValidCustomerObject()
        {
            // arrange
            controller.ModelState.AddModelError("some error name", "Error description");

            // act 
            Customer result = (Customer)((ViewResult)this.controller.Edit(1)).Model;

            // assert 
            Assert.AreEqual(this.customers[1], result);
        }


        [TestMethod]
        public void EditPostViewBagNotNull()
        {
            // arrange
            controller.ModelState.AddModelError("some error name", "Error description");

            // act 
            var result = (ViewResult)this.controller.Edit(1);

            // assert 
            Assert.IsNotNull(result.ViewBag.BoosterId);
        }

        [TestMethod]
        public void EditPostReturnValidPage()
        {
            // arrange
            controller.ModelState.AddModelError("some error name", "Error description");

            // act 
            var result = (ViewResult)this.controller.Edit(1);

            // assert 
            Assert.AreEqual("Edit", result.ViewName);
        }
        #endregion

        #region Delete
        //GET: Customers/Delete

        [TestMethod]
        public void DeleteGetCustomerIdisNull()
        {
            // arrange
            int? id = null;

            // act 
            var result = this.controller.Delete(id) as ViewResult;
            
            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteGetValidCustomerObject()
        {
            // act
            var result = ((ViewResult)this.controller.Delete(1)).Model as Customer;

            // assert
            Assert.AreEqual(this.customers[1], result);
        }

        [TestMethod]
        public void DeleteGetInvalidCustomerId()
        {
            // act
            var result = (ViewResult)this.controller.Delete(6);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteGetReturnValidPage()
        {
            // act
            var result = (ViewResult)this.controller.Delete(1);

            //assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        #endregion
    }
}
