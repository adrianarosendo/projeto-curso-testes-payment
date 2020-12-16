using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {

        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests (){

            _name = new Name("Bruce", "Wayne");
            _document = new Document ("37571256965", EDocumentType.CPF);
            _email = new Email ("batman@dc.com");
            _address = new Address("rua maria", "12", "mooca", "gotham", "SP", "BR", "123456986");
            _student = new Student (_name, _document, _email);
            _subscription = new Subscription(null);
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription(){

            var payment = new PayPalPayment("123456987",DateTime.Now, DateTime.Now.AddDays(5), 10,10, "WAYNE CORP", _document, _address, _email);
            
            _subscription.AddPayment(payment);
            
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            
            
            Assert.IsTrue(_student.Invalid);

        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment(){

            
            _student.AddSubscription(_subscription);

            
            
            Assert.IsTrue(_student.Invalid);

        }

        [TestMethod]
        public void ShouldReturnSuccessWhenHadNoActiveSubscription(){

            var payment = new PayPalPayment("123456987",DateTime.Now, DateTime.Now.AddDays(5), 10,10, "WAYNE CORP", _document, _address, _email);
            
            _subscription.AddPayment(payment);
            
            _student.AddSubscription(_subscription);
            

            
            
            Assert.IsTrue(_student.Valid);

        }
        
    }
}