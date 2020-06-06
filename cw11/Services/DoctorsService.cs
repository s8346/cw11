using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd11.Entities;

namespace apbd11.Services
{
    public class DoctorsService : IDoctorsService
    {
        private DoctorDbContext _context;
        public DoctorsService(DoctorDbContext context)
        {
            _context = context;
        }

        public List<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }

        public bool AddDoctor(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return true;
            } catch (Exception exc)
            {
                return false;
            }
            
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            try
            {
                var toUpdate = _context.Doctors.Where(d => d.IdDoctor == doctor.IdDoctor).First();
                toUpdate.FirstName = doctor.FirstName;
                toUpdate.LastName = doctor.LastName;
                toUpdate.Email = doctor.Email;
                _context.SaveChanges();
                return true;
            } catch (Exception exc)
            {
                return false;
            }
            
        }

        public bool DeleteDoctor(int id)
        {
            try
            {
                var toDelete = _context.Doctors.Where(d => d.IdDoctor == id).First();
                _context.Doctors.Remove(toDelete);
                _context.SaveChanges();
                return true;
            } catch (Exception exc)
            {
                return false;
            }
        }

        //Adding default sample data
        public void AddDefault()
        {
            try
            {
                var prescription1 = AddPrescriptionEntities1();
                AddMedicamentsEntities1(prescription1);
                var prescription2 = AddPrescriptionEntities2();
                AddMedicamentsEntities2(prescription2);
                _context.SaveChanges();
            } catch (Exception exc)
            {
                Console.WriteLine(exc.StackTrace);
            }
            
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
            _context.Patients.Add(patient);
            _context.Doctors.Add(doctor);
            _context.Prescriptions.Add(prescription);
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
            _context.Patients.Add(patient);
            _context.Doctors.Add(doctor);
            _context.Prescriptions.Add(prescription);
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
            _context.Medicaments.Add(medicament);
            _context.MedicamentPrescriptions.Add(mediacamentPrescription);
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
            _context.Medicaments.Add(medicament);
            _context.MedicamentPrescriptions.Add(mediacamentPrescription);
        }
    }
}
