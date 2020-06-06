using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd11.Entities;

namespace apbd11.Services
{
    public interface IDoctorsService
    {
        void AddDefault();

        List<Doctor> GetDoctors();

        bool AddDoctor(Doctor doctor);

        bool UpdateDoctor(Doctor doctor);

        bool DeleteDoctor(int id);
    }
}
