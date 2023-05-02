using Domain.Core;
using Moq.AutoMock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Tests.Application.Services
{
    public abstract class ServiceBaseTest
    {
        public IMapper _mapper { get; set; }
        public Mock<IUnitOfWork> _unitOfWork { get; set; }
        public AutoMocker _autoMocker { get; set; }

        public ServiceBaseTest()
        {
            _mapper = new AutoMapperFixture().GetMapper();
            _unitOfWork = new Mock<IUnitOfWork>();
            _autoMocker = new AutoMocker();
        }
    }
}
