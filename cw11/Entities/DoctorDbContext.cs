using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace apbd11.Entities
{
    public class DoctorDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<MedicamentPrescription> MedicamentPrescriptions { get; set; }

        public DoctorDbContext()
        {

        }
        public DoctorDbContext(DbContextOptions options): base(options)
        {
            
        }

        public void AddSampleData()
        {
            var prescription1 = AddPrescriptionEntities1();
            AddMedicamentsEntities1(prescription1);
            var prescription2 = AddPrescriptionEntities2();
            AddMedicamentsEntities2(prescription2);
            SaveChanges();
        }

        private Prescription AddPrescriptionEntities1()
        {
            var doctor = new Doctor()
            {
                //IdDoctor = 1,
                FirstName = "Arnold",
                LastName = "Brzeczukiewicz",
                Email = "ab@gmail.com"
            };
            var patient = new Patient()
            {
                //IdPatient = 1,
                FirstName = "Larry",
                LastName = "Brandenberg",
                BirthDate = new DateTime()
            };
            var prescription = new Prescription()
            {
                //IdPrescription = 1,
                Date = new DateTime(),
                DueDate = new DateTime(),
                Doctor = doctor,
                Patient = patient
            };
            Patients.Add(patient);
            Doctors.Add(doctor);
            Prescriptions.Add(prescription);
            return prescription;
        }

        private Prescription AddPrescriptionEntities2()
        {
            var doctor = new Doctor()
            {
                //IdDoctor = 2,
                FirstName = "Ferdynand",
                LastName = "Roche",
                Email = "fr@gmail.com"
            };
            var patient = new Patient()
            {
                //IdPatient = 2,
                FirstName = "Lawrenze",
                LastName = "Muller",
                BirthDate = new DateTime()
            };
            var prescription = new Prescription()
            {
                //IdPrescription = 1,
                Date = new DateTime(),
                DueDate = new DateTime(),
                Doctor = doctor,
                Patient = patient
            };
            Patients.Add(patient);
            Doctors.Add(doctor);
            Prescriptions.Add(prescription);
            return prescription;
        }

        private void AddMedicamentsEntities1(Prescription prescription)
        {
            var medicament = new Medicament()
            {
                //IdMedicament = 1,
                Name = "Paracetamol",
                Type = "Pill",
                Description = "Headache"
            };
            var mediacamentPrescription = new MedicamentPrescription()
            {
                Medicament = medicament,
                Prescription = prescription,
                Dose = 20,
                Details = "Obligatory"
            };
            Medicaments.Add(medicament);
            MedicamentPrescriptions.Add(mediacamentPrescription);
        }

        private void AddMedicamentsEntities2(Prescription prescription)
        {
            var medicament = new Medicament()
            {
                //IdMedicament = 2,
                Name = "Eurespal",
                Type = "Liquid",
                Description = "Throat treatment"
            };
            var mediacamentPrescription = new MedicamentPrescription()
            {
                Medicament = medicament,
                Prescription = prescription,
                Dose = 10,
                Details = "Not necessary"
            };
            Medicaments.Add(medicament);
            MedicamentPrescriptions.Add(mediacamentPrescription);
        }

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);
                entity.Property(e => e.IdDoctor);
                entity.ToTable("Doctor");

                entity.HasMany(doctor => doctor.Prescriptions)
                .WithOne(prescription => prescription.Doctor)
                .HasForeignKey(prescription => prescription.IdDoctor)
                .IsRequired();
            });

            builder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);
                entity.ToTable("Prescription");

                entity.HasMany(prescription => prescription.MedicamentPrescriptions)
                .WithOne(mp => mp.Prescription)
                .HasForeignKey(mp => mp.IdPrescription)
                .IsRequired();
            });
            builder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);
                entity.ToTable("Patient");

                entity.HasMany(patient => patient.Prescriptions)
                .WithOne(prescription => prescription.Patient)
                .HasForeignKey(prescription => prescription.IdPatient)
                .IsRequired();
            });
            builder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);
                entity.ToTable("Medicament");

                entity.HasMany(medicament => medicament.MedicamentPrescriptions)
                .WithOne(mp => mp.Medicament)
                .HasForeignKey(mp => mp.IdMedicament)
                .IsRequired();
            });
            builder.Entity<MedicamentPrescription>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.IdMedicament,
                    e.IdPrescription
                });
                
                entity.ToTable("MedicamentPrescription");
            });
        }
    }
}
