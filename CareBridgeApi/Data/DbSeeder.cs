using CareBridgeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CareBridgeApi.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(CareBridgeDBContext context)
        {
            if (await context.Patients.AnyAsync())
            {
                return;
            }

            var astarion = new Patient { FirstName = "Astarion", LastName = "Ancunin", DateOfBirth = new DateOnly(1913, 4, 16), Email = "astarion.ancunin@example.com", PhoneNumber = "555-013-7742" };
            var karlach = new Patient { FirstName = "Karlach", LastName = "Cliffgate", DateOfBirth = new DateOnly(1995, 7, 2), Email = "karlach.cliffgate@example.com", PhoneNumber = "555-014-2210" };
            var gale = new Patient { FirstName = "Gale", LastName = "Dekarios", DateOfBirth = new DateOnly(1988, 11, 23), Email = "gale.dekarios@example.com", PhoneNumber = "555-015-9031" };
            var laezel = new Patient { FirstName = "Lae'zel", LastName = "Githyanki", DateOfBirth = new DateOnly(2001, 3, 9), Email = "laezel@example.com", PhoneNumber = "555-016-4478" };
            var wyll = new Patient { FirstName = "Wyll", LastName = "Ravengard", DateOfBirth = new DateOnly(1997, 9, 14), Email = "wyll.ravengard@example.com", PhoneNumber = "555-017-6625" };

            var shadowheart = new Provider { FirstName = "Shadowheart", LastName = "Hallowleaf", Specialty = "Internal Medicine", Email = "shadowheart.hallowleaf@example.com", PhoneNumber = "555-019-2486" };
            var jaheira = new Provider { FirstName = "Jaheira", LastName = "Harper", Specialty = "Family Medicine", Email = "jaheira.harper@example.com", PhoneNumber = "555-020-3317" };
            var halsin = new Provider { FirstName = "Halsin", LastName = "Emerald", Specialty = "Cardiology", Email = "halsin.emerald@example.com", PhoneNumber = "555-021-8854" };

            context.Patients.AddRange(astarion, karlach, gale, laezel, wyll);
            context.Providers.AddRange(shadowheart, jaheira, halsin);

            context.Encounters.AddRange(

                new Encounter { Patient = astarion, Provider = shadowheart, StartDateTime = Utc(2026, 9, 1, 9, 0), EndDateTime = Utc(2026, 9, 1, 9, 45), EncounterType = "Outpatient", ReasonForVisit = "Annual physical", Status = "Completed" },
                new Encounter { Patient = karlach, Provider = halsin, StartDateTime = Utc(2026, 9, 3, 14, 0), EndDateTime = Utc(2026, 9, 3, 15, 0), EncounterType = "Outpatient", ReasonForVisit = "Palpitations follow-up", Status = "Completed" },
                new Encounter { Patient = karlach, Provider = halsin, StartDateTime = Utc(2026, 9, 20, 22, 30), EndDateTime = null, EncounterType = "Emergency", ReasonForVisit = "Chest pain", Status = "In Progress" },
                new Encounter { Patient = gale, Provider = jaheira, StartDateTime = Utc(2026, 9, 10, 11, 0), EndDateTime = Utc(2026, 9, 10, 11, 30), EncounterType = "Telehealth", ReasonForVisit = "Medication refill", Status = "Completed" },
                new Encounter { Patient = laezel, Provider = shadowheart, StartDateTime = Utc(2026, 9, 15, 8, 0), EndDateTime = Utc(2026, 9, 17, 10, 0), EncounterType = "Inpatient", ReasonForVisit = "Observation after injury", Status = "Completed" },
                new Encounter { Patient = wyll, Provider = jaheira, StartDateTime = Utc(2026, 10, 2, 13, 0), EndDateTime = null, EncounterType = "Outpatient", ReasonForVisit = "Vision check", Status = "Scheduled" }
            );
        }

        private static DateTime Utc(int year, int month, int day, int hour, int minute)
        {
            return new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
        }
    }
}
