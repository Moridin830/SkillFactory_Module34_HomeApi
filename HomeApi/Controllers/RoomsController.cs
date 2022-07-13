using System;
using System.Threading.Tasks;
using AutoMapper;
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
        
        //TODO: Задание - добавить метод на получение всех существующих комнат
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
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
        /// Обновление существующего устройства
        /// </summary>
        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> Edit(
            [FromRoute] string name,
            [FromBody] EditRoomRequest request)
        {
            var room = await _repository.GetRoomByName(name);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната {name} не подключена. Сначала подключите комнату!");

            var withSameName = await _repository.GetRoomByName(request.NewName);
            if (withSameName != null)
                return StatusCode(400, $"Ошибка: Комната с именем {request.NewName} уже подключена. Выберите другое имя!");

            await _repository.UpdateRoom(
                room,
                new UpdateRoomQuery(request.NewArea, request.NewGasConnected, request.NewVoltage, request.NewName)
            );

            return StatusCode(200, $"Комната обновлена! Имя - {request.NewName}, Этаж - {request.NewArea}");
        }
    }
}