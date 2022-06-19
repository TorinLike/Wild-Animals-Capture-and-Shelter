using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wild_Animals_Capture_and_Shelter.Model
{
    public static class DBInteractions
    {
        private static WACSEntities5 _entity = new WACSEntities5();

        public static List<Gender> GetGender()
        {
            return _entity.Gender.ToList();
        }

        public static List<Role> GetRole()
        {
            return _entity.Role.ToList();
        }

        public static List<Profile> GetProfile()
        {
            return _entity.Profile.ToList();
        }

        public static List<Animal> GetAnimal()
        {
            return _entity.Animal.ToList();
        }

        public static List<Shelter> GetShelter()
        {
            return _entity.Shelter.ToList();
        }
        public static List<Species> GetSpecies()
        {
            return _entity.Species.ToList();
        }
        public static List<CaptureHistory> GetCapture()
        {
            return _entity.CaptureHistory.ToList();
        }

        public static void SaveEditedAnimal(Animal animal)
        {
            _entity.SaveChanges();
        }

        public static void SaveEditedProfile(Profile profile)
        {
            _entity.SaveChanges();
        }

        public static void SaveProfile(Profile profile)
        {
            _entity.Profile.Add(profile);
            _entity.SaveChanges();
        }
        public static void SaveAnimal(Animal animal)
        {
            _entity.Animal.Add(animal);
            _entity.SaveChanges();
        }
        public static void SaveCapture(CaptureHistory chis)
        {
            _entity.CaptureHistory.Add(chis);
            _entity.SaveChanges();
        }


        public static void DeleteProfile(Profile profile)
        {
            _entity.Profile.Remove(profile);
            _entity.SaveChanges();
        }
        public static void DeleteAnimal(Animal animal)
        {
            _entity.Animal.Remove(animal);
            _entity.SaveChanges();
        }
        public static void DeleteCapture(CaptureHistory cs)
        {
            _entity.CaptureHistory.Remove(cs);
            _entity.SaveChanges();
        }
    }
}
