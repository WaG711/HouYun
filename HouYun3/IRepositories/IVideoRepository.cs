﻿using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideos();
        Task<Video> GetVideoById(int videoId);
        Task AddVideo(Video video);
        Task UpdateVideo(Video video);
        Task DeleteVideo(int videoId);
    }
}
