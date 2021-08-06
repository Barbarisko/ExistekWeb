using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    //Створити сервіси і підключити їх використовуючи Dependency injection
//Сервіси не повинні містити ніякої логіки але потрібно додати методи які умовно виконують певну дію.
//Серед сервісів повинні бути:
//- сервіс, який зберігає інформацію про статтю, яку потрібно опублікувати

    //LifeTime - Scoped
    public class ArticleService
    {
        private IInfoService infoService;
        public ArticleService(IInfoService _infoService)
        {
            infoService = _infoService;
        }

        //посчитать кол-во файлов в папке Артиклс, каждую вычитать в модель, вернуть список моделей
        //public IEnumerable<Article> GetAllArticles()
        //{
        //    return IEnumerable<Article> articles = _unitOfWork.ExhibitionRepository.GetAll();             
        //}

        
        public void AddNewExhibition(string name, int? price, string description)
        {
            Exhibition itemEntity = _unitOfWork.ExhibitionRepository.GetAll().ToList().Find(
                        i => i.Name == name);

            if (itemEntity != null)
            {
                throw new ArgumentException("This exhibition already exists! Find and edit it");
            }
            else
            {
                ExhibitionModel newItem = new ExhibitionModel()
                {
                    Name = name,
                    Price = price,
                    Description = description
                };

                Exhibition newItemEntity = _mapper.Map<Exhibition>(newItem);
                _unitOfWork.ExhibitionRepository.Add(newItemEntity);
            }
            _unitOfWork.Save();
        }

        public void DeleteExhibition(int exhId)
        {
            Exhibition exh = _unitOfWork.ExhibitionRepository.Get(exhId);
            if (exh == null)
            {
                throw new KeyNotFoundException();
            }
            _unitOfWork.ExhibitionRepository.Delete(exhId);
            _unitOfWork.Save();
        }

        public void UpdateEXHById(int Id, string name, int price, string desc)
        {
            List<Exhibition> sortedItemEntities = _unitOfWork.ExhibitionRepository.GetAll()
                .ToList().FindAll(i => i.Id == Id);
            if (sortedItemEntities.Any())
            {
                foreach (Exhibition i in sortedItemEntities)
                {
                    i.Name = name;
                    i.Price = price;
                    i.Description = desc;

                    _unitOfWork.ExhibitionRepository.Update(i);
                    _unitOfWork.Save();
                }
            }
        }
    }
}
