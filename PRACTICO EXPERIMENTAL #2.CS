using System;
using System.Collections.Generic;
using System.Threading;

namespace AuditoriumSeating
{
    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int SeatNumber { get; set; }

        public Attendee(int id, string name)
        {
            Id = id;
            Name = name;
            ArrivalTime = DateTime.Now;
            SeatNumber = -1;
        }
    }

    public class RegistrationDesk
    {
        public int DeskId { get; private set; }
        private Queue<Attendee> registrationQueue;

        public RegistrationDesk(int deskId)
        {
            DeskId = deskId;
            registrationQueue = new Queue<Attendee>();
        }

        public void AddToQueue(Attendee attendee)
        {
            registrationQueue.Enqueue(attendee);
        }

        public Attendee ProcessNextAttendee()
        {
            return registrationQueue.Count > 0 ? registrationQueue.Dequeue() : null;
        }

        public int GetQueueLength()
        {
            return registrationQueue.Count;
        }
    }

    public class Auditorium
    {
        private readonly int totalSeats;
        private bool[] seats;
        private List<RegistrationDesk> registrationDesks;
        private List<Attendee> registeredAttendees;

        public Auditorium(int totalSeats)
        {
            this.totalSeats = totalSeats;
            seats = new bool[totalSeats];
            registrationDesks = new List<RegistrationDesk>
            {
                new RegistrationDesk(1),
                new RegistrationDesk(2)
            };
            registeredAttendees = new List<Attendee>();
        }

        public int AssignSeat()
        {
            for (int i = 0; i < totalSeats; i++)
            {
                if (!seats[i])
                {
                    seats[i] = true;
                    return i + 1;
                }
            }
            return -1;
        }

        public void ProcessRegistrations()
        {
            while (registeredAttendees.Count < totalSeats)
            {
                foreach (var desk in registrationDesks)
                {
                    var attendee = desk.ProcessNextAttendee();
                    if (attendee != null)
                    {
                        int seatNumber = AssignSeat();
                        if (seatNumber != -1)
                        {
                            attendee.SeatNumber = seatNumber;
                            registeredAttendees.Add(attendee);
                            Console.WriteLine($"Desk {desk.DeskId}: Assigned seat {seatNumber} to Attendee {attendee.Name}");
                        }
                    }
                    Thread.Sleep(100); // Simula el tiempo de procesamiento
                }
            }
        }

        public void GenerateReport()
        {
            Console.WriteLine("\nAuditorium Seating Report");
            Console.WriteLine("-------------------------");
            foreach (var attendee in registeredAttendees)
            {
                Console.WriteLine($"Seat {attendee.SeatNumber}: {attendee.Name} (Arrived: {attendee.ArrivalTime})");
            }
            Console.WriteLine($"\nTotal Seats Assigned: {registeredAttendees.Count}/{totalSeats}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Auditorium auditorium = new Auditorium(100);
            
            // Simulación de llegada de asistentes
            Random random = new Random();
            for (int i = 1; i <= 120; i++) // Más personas que asientos para probar el límite
            {
                var attendee = new Attendee(i, $"Attendee{i}");
                auditorium.registrationDesks[random.Next(2)].AddToQueue(attendee);
            }

            // Procesar registros
            auditorium.ProcessRegistrations();
            
            // Generar reporte
            auditorium.GenerateReport();
        }
    }
}