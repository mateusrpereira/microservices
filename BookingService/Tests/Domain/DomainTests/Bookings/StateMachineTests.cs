using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);//n�o importado corretamente, declarativo...
            Assert.AreEqual(booking.Status, Status.Paid);
        }

        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);
            
            Assert.AreEqual(booking.Status, Status.Canceled);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            
            Assert.AreEqual(booking.Status, Status.Finished);
        }

        [Test]
        public void ShouldSetStatusToRefoundedWhenRefoundingAPaidBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.Status, Status.Refounded);
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopeningACanceledBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.AreEqual(booking.Status, Status.Created);
        }

        //Negative Tests

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusWhenRefoundingAFinishedBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.AreEqual(booking.Status, Status.Finished);
        }
    }
}