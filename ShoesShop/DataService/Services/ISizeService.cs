using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataService.ViewModels;
using AutoMapper;

namespace DataService.Services
{
    public interface ISizeService
    {
        List<Size> GetAllSize();
        void InitSize();
    }

    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            this._sizeRepository = sizeRepository;
        }

        public List<Size> GetAllSize()
        {
            List<Size> sizes = _sizeRepository.GetAll().ToList();

            return sizes;
        }

        public void InitSize()
        {
            for (int i = 30; i < 50; i++)
            {
                _sizeRepository.Add(new Size()
                {
                    Size1 = i
                });
            }
        }
    }
}
