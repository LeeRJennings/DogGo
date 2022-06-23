using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalksRepository
    {
        List<Walks> GetWalksByWalker(int walkerId);
    }
}
