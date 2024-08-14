using Business.Repository.IRepositpry;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Repository
{
    public class HotelImagesRepository : IHotelImageRepository
    {
        private readonly AppilicationDbContext _db;
        public HotelImagesRepository(AppilicationDbContext db)
        {
            _db = db; 
        }
        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDto)
        {
            HotelRoomImage hotelRoomImage = new HotelRoomImage
            {
                Id = imageDto.Id,
                RoomId = imageDto.RoomId,
                RoomImageUrl = imageDto.RoomImageUrl,  
            };
            await _db.HotelRoomImages.AddAsync(hotelRoomImage);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelByImageUrl(string imageUrl)
        {
            var allImages = await _db.HotelRoomImages.FirstOrDefaultAsync
                                (x=>x.RoomImageUrl.ToLower()==imageUrl.ToLower());
            _db.HotelRoomImages.Remove(allImages);
            return await _db.SaveChangesAsync();
        }


        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FindAsync(imageId);
            _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.HotelRoomImages.Where(x=>x.RoomId==roomId).ToListAsync();
            _db.HotelRoomImages.RemoveRange(imageList);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImage(int roomId)
        {
            var hotelimages = await _db.HotelRoomImages.Where(x=>x.RoomId== roomId).ToListAsync();

            var hotelImageDTOs = hotelimages.Select(hotelimage => new HotelRoomImageDTO
            {
                Id = hotelimage.Id,
                RoomId = hotelimage.RoomId,
                RoomImageUrl = hotelimage.RoomImageUrl,
            });
            return hotelImageDTOs;
        }
    }
}
