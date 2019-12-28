using System;
using BeepBong.Domain.Models;

namespace BeepBong.Application.Interfaces
{
    public interface IImageEntity
    {
        Image Image { get; set; }
        Guid? ImageId { get; set; }
        bool ImageChange { get; set; }
    }
}