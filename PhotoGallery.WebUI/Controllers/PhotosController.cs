using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Model;
using PhotoGallery.Infrastructure.Core;
using PhotoGallery.ViewModels;

namespace PhotoGallery.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class PhotosController : Controller
    {
        readonly IPhotoRepository _photoRepository;
        readonly ILoggingRepository _loggingRepository;
        public PhotosController(IPhotoRepository photoRepository, ILoggingRepository loggingRepository)
        {
            _photoRepository = photoRepository;
            _loggingRepository = loggingRepository;
        }

        [HttpGet]
        public PaginationSet<PhotoViewModel> Get(int? page, int? pageSize)
        {
            PaginationSet<PhotoViewModel> pagedSet = null;

            try
            {
                if (page != null)
                {
                    int currentPage = page.Value;
                    if (pageSize != null)
                    {
                        int currentPageSize = pageSize.Value;

                        List<Photo> photos = null;
                        int totalPhotos = new int();


                        photos = _photoRepository
                            .AllIncluding(p => p.Album)
                            .OrderBy(p => p.Id)
                            .Skip(currentPage * currentPageSize)
                            .Take(currentPageSize)
                            .ToList();

                        totalPhotos = _photoRepository.GetAll().Count();

                        IEnumerable<PhotoViewModel> photosVm = Mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoViewModel>>(photos);

                        pagedSet = new PaginationSet<PhotoViewModel>()
                        {
                            Page = currentPage,
                            TotalCount = totalPhotos,
                            TotalPages = (int)Math.Ceiling((decimal)totalPhotos / currentPageSize),
                            Items = photosVm
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            return pagedSet;
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                Photo photoToRemove = this._photoRepository.GetSingle(id);
                this._photoRepository.Delete(photoToRemove);
                this._photoRepository.Commit();
                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
                return Json(new { status = false });
            }
        }
    }
}
