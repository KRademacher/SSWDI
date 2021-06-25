using Core.DomainModel;
using DomainServices.Repositories;
using System;
using System.Collections.Generic;

namespace HttpData
{
    public class HttpLodgingRepository : ILodgingRepository
    {
        public Lodging Create(Lodging lodging)
        {
            throw new InvalidOperationException("Create operation not permitted.");
        }

        public void Delete(Lodging lodging)
        {
            throw new InvalidOperationException("Delete operation not permitted.");
        }

        public IEnumerable<Lodging> GetAll()
        {
            return Globals.HttpGet<List<Lodging>>("/api/lodging");
        }

        public IEnumerable<Animal> GetAnimalsInLodge(int id)
        {
            return GetByID(id).LodgingAnimals;
        }

        public Lodging GetByID(int id)
        {
            return Globals.HttpGet<Lodging>($"/api/lodging/{id}");
        }

        public void Update(Lodging lodging)
        {
            Globals.HttpPut(lodging, $"/api/lodging/{lodging.ID}");
        }
    }
}