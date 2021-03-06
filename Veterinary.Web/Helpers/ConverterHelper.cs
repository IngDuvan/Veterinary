﻿using System.Threading.Tasks;
using Veterinary.Common.Models;
using Veterinary.Web.Data.Entities;
using Veterinary.Web.Models;
using Veterinary.Web.Models.Data;

namespace Veterinary.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public async Task<Pet> ToPetAsync(PetViewModel model, string path, bool isNew)
        {
            var pet = new Pet
            {
                Agendas = model.Agendas,
                Born = model.Born,
                Histories = model.Histories,
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Age = model.Age,
                Reproductive = model.Reproductive,
                Food = model.Food,
                Color = model.Color,
                Frequency = model.Frequency,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                PetType = await _dataContext.PetTypes.FindAsync(model.PetTypeId),
                PetSex = await _dataContext.PetSexes.FindAsync(model.PetSexId),
                PetRace = await _dataContext.PetRaces.FindAsync(model.PetRaceId),
                Remarks = model.Remarks,
            };

            return pet;
        }


        public PetViewModel ToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Agendas = pet.Agendas,
                Born = pet.Born,
                Histories = pet.Histories,
                ImageUrl = pet.ImageUrl,
                Name = pet.Name,
                Owner = pet.Owner,
                PetType = pet.PetType,
                Remarks = pet.Remarks,
                Id = pet.Id,
                OwnerId = pet.Owner.Id,
                PetTypeId = pet.PetType.Id,
                PetSexId = pet.PetSex.Id,
                PetRaceId = pet.PetRace.Id,
                Reproductive = pet.Reproductive,
                Age = pet.Age,
                Color = pet.Color,
                Food = pet.Food,
                Frequency = pet.Frequency,
                PetRaces = _combosHelper.GetComboPetRace(),
                PetSexs = _combosHelper.GetComboPetSex(),
                PetTypes = _combosHelper.GetComboPetTypes(),

            };

        }

        public async Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew)
        {

            //TODO: organizar fechas al momenton de ver y guardar una historia
            return new History
            {
                Date = model.Date.ToUniversalTime(),
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                Pet = await _dataContext.Pets.FindAsync(model.PetId),
                Remarks = model.Remarks,
                ServiceType = await _dataContext.ServiceTypes.FindAsync(model.ServiceTypeId)
            };

        }

        public HistoryViewModel ToHistoryViewModel(History history)
        {
            return new HistoryViewModel
            {
                Date = history.Date,
                Description = history.Description,
                Id = history.Id,
                PetId = history.Pet.Id,
                Remarks = history.Remarks,
                ServiceTypeId = history.ServiceType.Id,
                ServiceTypes = _combosHelper.GetComboServiceTypes()
            };

        }

        public PetResponse ToPetResponse(Pet pet)
        {
            if (pet == null)
            {
                return null;
            }

            return new PetResponse
            {
                Born = pet.Born,
                Id = pet.Id,
                ImageUrl = pet.ImageFullPath,
                Name = pet.Name,
                PetType = pet.PetType.Name,
                PetRace = pet.PetRace.Name,
                PetSex = pet.PetSex.Name,
                Remarks = pet.Remarks
            };
        }

        public OwnerResponse ToOwnerResposne(Owner owner)
        {
            if (owner == null)
            {
                return null;
            }

            return new OwnerResponse
            {
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                PhoneNumber = owner.User.PhoneNumber
            };
        }

    }
}
