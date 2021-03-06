﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VSFlyEFCoreApp;

namespace VSFlyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly WWWingsContext _context;

        public BookingsController(WWWingsContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingSet()
        {
            var bookingList = await _context.BookingSet.ToListAsync();
            List<Booking> bookingListTmp = new List<Booking>();
            foreach (Booking b in bookingList)
            {
                Booking bM = new Booking();
                bM.FlightNo = b.FlightNo;
                bM.PassengerID = b.PassengerID;
                bookingListTmp.Add(bM);
            }
            return bookingListTmp;
        }


        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.BookingSet.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // Not used
        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.BookingSet.Add(booking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingExists(booking.FlightNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBooking", new { id = booking.FlightNo }, booking);
        }

        // Not used
        // DELETE: api/Bookings/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.BookingSet.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.BookingSet.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool BookingExists(int id)
        {
            return _context.BookingSet.Any(e => e.FlightNo == id);
        }

        // ----------------------------
        
    }
}
