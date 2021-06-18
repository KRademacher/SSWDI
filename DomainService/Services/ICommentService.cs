﻿using Core.DomainModel;
using DomainServices.Repositories;
using System.Collections.Generic;

namespace DomainServices.Services
{
    public interface ICommentService : ICommentRepository
    {
        public IEnumerable<Comment> GetRelatedComments(int animalId);
    }
}