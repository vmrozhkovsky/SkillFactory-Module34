﻿using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("AddRoom")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
        
        /// <summary>
        /// Просмотр списка комнат
        /// </summary>
        [HttpGet] 
        [Route("GetAllRooms")] 
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repository.GetRooms();

            var resp = new GetRoomsResponse()
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };
            
            return StatusCode(200, resp);
        }
        
        /// <summary>
        /// Обновление существующей комнаты
        /// </summary>
        [HttpPatch] 
        [Route("EditRoom")] 
        public async Task<IActionResult> Edit(
            string roomName,
            [FromBody]  EditRoomRequest request)
        {
            var room = await _repository.GetRoomByName(roomName);
            if(room == null)
                return StatusCode(400, $"Ошибка: Комнаты с именем {roomName} не существует.");

            await _repository.UpdateRoom(
                room,
                new UpdateRoomQuery(request.NewArea, request.NewGasConnected, request.NewVoltage, request.NewName)
            );

            return StatusCode(200, $"Комната обновлена! Имя - {room.Name}, номер - {room.Area}, вольтаж - {room.Voltage}");
        }
    }
}