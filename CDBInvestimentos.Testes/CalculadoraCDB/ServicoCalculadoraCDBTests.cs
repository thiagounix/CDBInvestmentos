using CdbInvestimentos.Aplicacao.Handlers;
using CdbInvestimentos.Aplicacao.Interfaces;
using CdbInvestimentos.Aplicacao.Responses;
using CdbInvestimentos.Aplicacao.Services.Calculators;
using Moq;

namespace CdbInvestimentos.CdbInvestimentos.Testes
{
    public class CalcularCdbCommandHandlerTests
    {
        private readonly CalcularCdbCommandHandler _handler;
        private readonly Mock<IServicoCalculadoraCdb> _mockServicoCalculadoraCdb;
        private readonly IServicoCalculadoraCdb _servicoCalculadoraCdb;

        public CalcularCdbCommandHandlerTests()
        {
            _mockServicoCalculadoraCdb = new Mock<IServicoCalculadoraCdb>();
            _handler = new CalcularCdbCommandHandler(_mockServicoCalculadoraCdb.Object);
            _servicoCalculadoraCdb = new ServicoCalculadoraCdb();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsExpectedResult()
        {
            var command = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 12 };
            var expectedResponse = new CdbCalculoResultado { ValorBruto = 1200, ValorLiquido = 1100 };

            _mockServicoCalculadoraCdb
                .Setup(service => service.CalcularCdbAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.Equal(expectedResponse.ValorBruto, resultado.ValorBruto);
            Assert.Equal(expectedResponse.ValorLiquido, resultado.ValorLiquido);
        }

        [Theory]
        [InlineData(0, 5, "O valor inicial deve ser maior que zero.")]
        [InlineData(-1000, 10, "O valor inicial deve ser maior que zero.")]
        [InlineData(1000, 0, "O prazo em meses deve ser maior que 0.")]
        [InlineData(1000, 1201, "O prazo em meses é muito alto. O máximo permitido é 1200 meses.")]
        public async Task Handle_InvalidCommand_ThrowsArgumentException(decimal valorInicial, int prazoMeses, string expectedMessage)
        {
            var command = new CalcularCdbCommand { ValorInicial = valorInicial, PrazoMeses = prazoMeses };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, default));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task Handle_ShortTermInvestment_AppliesCorrectTaxRate()
        {
            var command = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 5 };
            var expectedResponse = new CdbCalculoResultado { ValorBruto = 1050, ValorLiquido = 900 };

            _mockServicoCalculadoraCdb
                .Setup(service => service.CalcularCdbAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
            Assert.Equal(expectedResponse.ValorLiquido, resultado.ValorLiquido);
        }

        [Fact]
        public async Task Handle_LongTermInvestment_AppliesCorrectTaxRate()
        {
            var command = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 25 };
            var expectedValorBruto = 1300m;
            var expectedValorLiquido = expectedValorBruto * 0.85m;

            var expectedResponse = new CdbCalculoResultado
            {
                ValorBruto = expectedValorBruto,
                ValorLiquido = expectedValorLiquido
            };

            _mockServicoCalculadoraCdb
                .Setup(service => service.CalcularCdbAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.Equal(expectedValorBruto, resultado.ValorBruto);
            Assert.Equal(expectedValorLiquido, resultado.ValorLiquido);
        }

        [Fact]
        public async Task Handle_BoundaryTermInvestment_AppliesCorrectTaxRate()
        {
            var command = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 6 };
            var expectedResponse = new CdbCalculoResultado { ValorBruto = 1100, ValorLiquido = 880 };

            _mockServicoCalculadoraCdb
                .Setup(service => service.CalcularCdbAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
            Assert.Equal(expectedResponse.ValorLiquido, resultado.ValorLiquido);
        }

        [Fact]
        public async Task Handle_MaximumTermInvestment_AppliesLongTermTax()
        {
            var command = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 1200 };
            var expectedValorBruto = 1099422640.03m;
            var expectedValorLiquido = 934510744.08m;

            var expectedResponse = new CdbCalculoResultado
            {
                ValorBruto = expectedValorBruto,
                ValorLiquido = expectedValorLiquido
            };

            _mockServicoCalculadoraCdb
                .Setup(service => service.CalcularCdbAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.Equal(expectedValorBruto, resultado.ValorBruto);
            Assert.Equal(expectedValorLiquido, resultado.ValorLiquido);
        }


        [Theory]
        [InlineData(1000, 12, 20.0)]

        public async Task CalcularCdbAsync_ValorLiquidoCalculadoCorretamente(decimal valorInicial, int prazoMeses, double taxaImposto)
        {

            decimal valorBrutoEsperado = CdbCalculator.CalcularValorBruto(valorInicial, prazoMeses);
            decimal valorLiquidoEsperado = CdbCalculator.CalcularValorLiquido(valorBrutoEsperado, valorInicial, (decimal)taxaImposto);

            // Act
            var resultado = await _servicoCalculadoraCdb.CalcularCdbAsync(valorInicial, prazoMeses);

            // Assert
            Assert.Equal(valorBrutoEsperado, resultado.ValorBruto);
            Assert.Equal(valorLiquidoEsperado, resultado.ValorLiquido);
        }

    }
}