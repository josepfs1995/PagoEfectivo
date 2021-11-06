using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PagoEfectivo.Api.Commands.PromocionCommand;
using PagoEfectivo.Api.Domain.Commands.PromocionCommand;
using PagoEfectivo.UnitTest.FakerFixtures;
using Xunit;

namespace PagoEfectivo.UnitTest
{
    [Collection(nameof(FakerCollection))]
    public class PromocionTests
    {
        private readonly FakerFixture _fakerFixture;
        public PromocionTests(FakerFixture fakerFixture)
        {
            _fakerFixture = fakerFixture;
        }
        [Fact]
        public async Task PromocionTest_Adicionar_DebeEjecutarConExito()
        {
            var _context = _fakerFixture.GenerarDbContext();
            var promocionCommand = new CrearPromocionCommand("Josep", "Josepfs_1995@hotmail.com");
            var mediator = new Mock<PromocionCommandHandler>(_context).Object;
            var response = await mediator.Handle(promocionCommand, new CancellationToken());
            response.IsValid.Should().BeTrue();
        }
        [Fact]
        public async Task PromocionTest_AdicionarRepetido_DebeEjecutarConError()
        {
            var _context = _fakerFixture.GenerarDbContext();
            var promocionCommand = new CrearPromocionCommand("Josep", "repetido@hotmail.com");
            var mediator = new Mock<PromocionCommandHandler>(_context).Object;
            var response = await mediator.Handle(promocionCommand, new CancellationToken());
            response.IsValid.Should().BeFalse();
        }
        [Fact]
        public async Task PromocionTest_Canjear_DebeEjecutarConExito()
        {
            var _context = _fakerFixture.GenerarDbContext();
            var promocionCommand = new CanjearPromocionCommand("ABCDEF");
            var mediator = new Mock<PromocionCommandHandler>(_context).Object;
            var response = await mediator.Handle(promocionCommand, new CancellationToken());
            response.IsValid.Should().BeTrue();
        }
        [Fact]
        public async Task PromocionTest_CanjearError_DebeEjecutarConError()
        {
            var _context = _fakerFixture.GenerarDbContext();
            var promocionCommand = new CanjearPromocionCommand("NOEXISTE");
            var mediator = new Mock<PromocionCommandHandler>(_context).Object;
            var response = await mediator.Handle(promocionCommand, new CancellationToken());
            response.IsValid.Should().BeFalse();
        }
    }


}
