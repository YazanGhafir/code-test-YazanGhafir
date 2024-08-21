using Hedin.ChangeTires.Api.Requests.Booking;
using System;
using Xunit;

namespace Tests.Requests;

public class BookingRequestTests
{
    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("invalid-email", false)]
    public void TestEmailValidation(string email, bool isValid)
    {
        var bookingRequest = new BookingRequest();
        if (isValid)
        {
            bookingRequest.CustomerEmail = email;
            Assert.Equal(email, bookingRequest.CustomerEmail);
        }
        else
        {
            Assert.Throws<ArgumentException>(() => bookingRequest.CustomerEmail = email);
        }
    }

    [Fact]
    public void TimeSlot_ShouldThrow_WhenLessThan24HoursInAdvance()
    {
        var bookingRequest = new BookingRequest();
        var testTime = DateTime.Now.AddHours(23);

        Assert.Throws<ArgumentException>(() => bookingRequest.TimeSlot = testTime);
    }

    [Fact]
    public void TimeSlot_ShouldThrow_WhenMoreThan30DaysAhead()
    {
        var bookingRequest = new BookingRequest();
        var testTime = DateTime.Now.AddDays(31);

        Assert.Throws<ArgumentException>(() => bookingRequest.TimeSlot = testTime);
    }

    [Fact]
    public void TimeSlot_ShouldNotThrow_WhenWithinValidRange()
    {
        var bookingRequest = new BookingRequest();

        // Find a future date that is at least 24 hours ahead, and set a time well within the weekday operating hours.
        // For simplicity, we'll choose a time like 10:00 AM on the next Wednesday that meets the advance booking requirement.
        var validTimeWithinOperatingHours = FindNextDayOfWeek(DateTime.Now.AddDays(2), DayOfWeek.Wednesday).AddHours(10); // 10:00 AM

        // Ensure the date also respects the "not more than 30 days ahead" rule.
        if (validTimeWithinOperatingHours > DateTime.Now.AddDays(30))
        {
            validTimeWithinOperatingHours = DateTime.Now.AddDays(29).AddHours(10); // Adjust to fall within 30 days
        }

        var exception = Record.Exception(() => bookingRequest.TimeSlot = validTimeWithinOperatingHours);
        Assert.Null(exception);
    }


    [Fact]
    public void TimeSlot_ShouldNotThrow_WhenWithinOperatingHoursAndValidAdvance()
    {
        var bookingRequest = new BookingRequest();
        // Find next Monday, ensuring it's at least 24 hours in advance
        var nextMonday = FindNextDayOfWeek(DateTime.Now.AddDays(1), DayOfWeek.Monday).AddHours(9); // 9:00 AM is within operating hours

        var exception = Record.Exception(() => bookingRequest.TimeSlot = nextMonday);
        Assert.Null(exception);
    }

    [Fact]
    public void TimeSlot_ShouldThrow_WhenOutsideOperatingHours()
    {
        var bookingRequest = new BookingRequest();
        // Calculate a valid future date, but set a time definitely outside operating hours (e.g., 6:00 PM on a weekday)
        var invalidTime = FindNextDayOfWeek(DateTime.Now.AddDays(1), DayOfWeek.Monday).AddHours(18); // 6:00 PM

        var exception = Assert.Throws<ArgumentException>(() => bookingRequest.TimeSlot = invalidTime);
        Assert.Contains("outside operating hours", exception.Message);
    }

    [Fact]
    public void TimeSlot_ShouldThrow_WhenOnSunday()
    {
        var bookingRequest = new BookingRequest();
        // Find next Sunday, ensuring it's at least 24 hours in advance
        var nextSunday = FindNextDayOfWeek(DateTime.Now.AddDays(1), DayOfWeek.Sunday).AddHours(10); // 10:00 AM

        Assert.Throws<ArgumentException>(() => bookingRequest.TimeSlot = nextSunday);
    }

    // Helper method to find the next occurrence of a specific day
    private DateTime FindNextDayOfWeek(DateTime start, DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return daysToAdd == 0 ? start.AddDays(7) : start.AddDays(daysToAdd);
    }
}
