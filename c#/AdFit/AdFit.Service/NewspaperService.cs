using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using AdFit.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Service
{
    public class NewspaperService:INewspaperService
    {

        private readonly INewspaperRepository _newspaperRepository;
        public NewspaperService(INewspaperRepository newspaperRepository)
        {
            _newspaperRepository = newspaperRepository;
        }
        public List<Newspaper> GetAll()
        {
            return _newspaperRepository.GetNewspapers();
        }
        public Newspaper AddNewspaper(Newspaper newspaper)
        {
            return _newspaperRepository.AddNewspaper(newspaper);
        }
        public Newspaper UpdateNewspaper(int id, Newspaper newspaper)
        {
            return _newspaperRepository.UpdateNewspaper(id, newspaper);
        }
        public void DeleteNewspaper(int id)
        {
            _newspaperRepository.DeleteNewspaper(id);
        }
    }
}
