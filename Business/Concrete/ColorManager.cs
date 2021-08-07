using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(),"All colors listed.");
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == id),"Color listed by id.");
        }

        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult("Color added successfully.");
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult("Color updated successfully.");
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult("Color deleted successfully.");
        }
    }
}
