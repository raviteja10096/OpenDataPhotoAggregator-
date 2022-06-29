using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAggregatorApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace PhotoAggregatorApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoAggregatorController : Controller
    {

        private readonly ILogger<PhotoAggregatorController> _logger;

        List<PhotoAggregatorModel> photoList = new List<PhotoAggregatorModel>();

        List<AlbumAggregatorModel> albumList = new List<AlbumAggregatorModel>();

        String photosendPoint = "http://jsonplaceholder.typicode.com/photos";
        String userAlbumsendPoint =  "http://jsonplaceholder.typicode.com/users/";

        String albumsendPoint = "http://jsonplaceholder.typicode.com/albums";

        public PhotoAggregatorController(ILogger<PhotoAggregatorController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public List<PhotoAlbum> Get()
        {
            photoList = GetPhotodata();
            albumList = GetAlbumsdata();
            return GetFilteredData(albumList, photoList);
        }

        [HttpGet]
        [Route("{id:int}")]
        public List<PhotoAlbum> GetbyId(int id)
        {
            photoList = GetPhotodata();
            albumList = GetAlbumsdatabyUser(id);
            return GetFilteredData(albumList, photoList);
        }

        private List<PhotoAlbum> GetFilteredData(List<AlbumAggregatorModel> albmList,List<PhotoAggregatorModel> photoList)
        {
            List<PhotoAlbum> photoAlbumList = new List<PhotoAlbum>();
            foreach (var a in albumList)
            {
                PhotoAlbum photoAlbum = new PhotoAlbum();
                photoAlbum.AlbumID = a.ID;
                photoAlbum.UserID = a.UserID;
                photoAlbum.Title = a.Title;
                photoAlbum.Photos = (photoList.Where(x => x.AlbumID == a.ID).ToList());
                photoAlbumList.Add(photoAlbum);
            }
            return photoAlbumList;
        }

        private List<PhotoAggregatorModel> GetPhotodata()
        {
            List<PhotoAggregatorModel> photosmodelList = new List<PhotoAggregatorModel>();
            var request = (HttpWebRequest)WebRequest.Create(photosendPoint);
            request.Method = HttpMethod.Get.ToString();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            var data = reader.ReadToEnd().ToString();
                            photosmodelList = JsonConvert.DeserializeObject<List<PhotoAggregatorModel>>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return photosmodelList;
        }

        private List<AlbumAggregatorModel> GetAlbumsdata()
        {
            List<AlbumAggregatorModel> albumsmodelList = new List<AlbumAggregatorModel>();
            var request = (HttpWebRequest)WebRequest.Create(albumsendPoint);
            request.Method = HttpMethod.Get.ToString();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            var data = reader.ReadToEnd().ToString();
                            albumsmodelList = JsonConvert.DeserializeObject<List<AlbumAggregatorModel>>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return albumsmodelList;
        }

        private List<AlbumAggregatorModel> GetAlbumsdatabyUser(int userid)
        {
            List<AlbumAggregatorModel> albumsmodelList = new List<AlbumAggregatorModel>();
            var request = (HttpWebRequest)WebRequest.Create(userAlbumsendPoint+userid+"/albums");
            request.Method = HttpMethod.Get.ToString();
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            var data = reader.ReadToEnd().ToString();
                            albumsmodelList = JsonConvert.DeserializeObject<List<AlbumAggregatorModel>>(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }
            return albumsmodelList;
        }
    }
}
