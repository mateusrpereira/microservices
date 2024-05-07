using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            Status = Status.Created;
            PlacedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        public Status Status { get; set; }

        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paid,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paid, Action.Finish) => Status.Finished,
                (Status.Paid, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        public bool IsValid()
        {
            try
            {
                ValidateState();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ValidateState()
        {
            if (PlacedAt == default)
            {
                throw new PlacedAtIsARequiredInformationException();
            }

            if (Start == default)
            {
                throw new StartDateTimeIsRequiredException();
            }

            if (End == default)
            {
                throw new EndDateTimeIsRequiredException();
            }

            if (Room == null)
            {
                throw new RoomIsRequiredException();
            }

            if (Guest == null)
            {
                throw new GuestIsRequiredException();
            }
        }

        public async Task Save(IBookingRepository bookingRepository)
        {
            ValidateState();

            Guest.IsValid();

            if (!Room.CanBeBooked())
            {
                throw new RoomCannotBeBookedException();
            }

            if (Id == 0)
            {
                var res = await bookingRepository.Create(this);
                Id = res.Id;
            }
            else
            {

            }
        }
    }
}
