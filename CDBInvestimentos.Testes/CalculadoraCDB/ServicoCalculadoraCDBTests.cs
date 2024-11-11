using CDBInvestimentos.Aplicacao.Handlers;
using CDBInvestimentos.Aplicacao.Interfaces;
using CDBInvestimentos.Aplicacao.Responses;
using CDBInvestimentos.Aplicacao.Services.Calculators;
using Moq;

namespace CDBInvestimentos.CDBInvestimentos.Testes
{
    public class CalcularCDBCommandHandlerTests
    {
        private readonly CalcularCDBCommandHandler _handler;
        private readonly Mock<IServicoCalculadoraCDB> _mockServicoCalculadoraCDB;
        private readonly IServicoCalculadoraCDB _servicoCalculadoraCDB;

        public CalcularCDBCommandHandlerTests()
        {
            _mockServicoCalculadoraCDB = new Mock<IServicoCalculadoraCDB>();
            _handler = new CalcularCDBCommandHandler(_mockServicoCalculadoraCDB.Object);
            _servicoCalculadoraCDB = new ServicoCalculadoraCDB();
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsExpectedResult()
        {
            var command = new CalcularCDBCommand { ValorInicial = 1000, PrazoMeses = 12 };
            var expectedResponse = new CDBCalculoResultado { ValorBruto = 1200, ValorLiquido = 1100 };

            _mockServicoCalculadoraCDB
                .Setup(service => service.CalcularCDBAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.Equal(expectedResponse.ValorBruto, resultado.ValorBruto);
            Assert.Equal(expectedResponse.ValorLiquido, resultado.ValorLiquido);
        }

        [Theory]
        [InlineData(0, 5, "O valor inicial deve ser maior que zero.")]
        [InlineData(-1000, 10, "O valor inicial deve ser maior que zero.")]
        [InlineData(1000, 0, "O prazo em meses deve ser maior que 1.")]
        [InlineData(1000, 1201, "O prazo em meses é muito alto. O máximo permitido é 1200 meses.")]
        public async Task Handle_InvalidCommand_ThrowsArgumentException(decimal valorInicial, int prazoMeses, string expectedMessage)
        {
            var command = new CalcularCDBCommand { ValorInicial = valorInicial, PrazoMeses = prazoMeses };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, default));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public async Task Handle_ShortTermInvestment_AppliesCorrectTaxRate()
        {
            var command = new CalcularCDBCommand { ValorInicial = 1000, PrazoMeses = 5 };
            var expectedResponse = new CDBCalculoResultado { ValorBruto = 1050, ValorLiquido = 900 };

            _mockServicoCalculadoraCDB
                .Setup(service => service.CalcularCDBAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
            Assert.Equal(expectedResponse.ValorLiquido, resultado.ValorLiquido);
        }

        [Fact]
        public async Task Handle_LongTermInvestment_AppliesCorrectTaxRate()
        {
            var command = new CalcularCDBCommand { ValorInicial = 1000, PrazoMeses = 25 };
            var expectedValorBruto = 1300m;
            var expectedValorLiquido = expectedValorBruto * 0.85m;

            var expectedResponse = new CDBCalculoResultado
            {
                ValorBruto = expectedValorBruto,
                ValorLiquido = expectedValorLiquido
            };

            _mockServicoCalculadoraCDB
                .Setup(service => service.CalcularCDBAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.Equal(expectedValorBruto, resultado.ValorBruto);
            Assert.Equal(expectedValorLiquido, resultado.ValorLiquido);
        }

        [Fact]
        public async Task Handle_BoundaryTermInvestment_AppliesCorrectTaxRate()
        {
            var command = new CalcularCDBCommand { ValorInicial = 1000, PrazoMeses = 6 };
            var expectedResponse = new CDBCalculoResultado { ValorBruto = 1100, ValorLiquido = 880 };

            _mockServicoCalculadoraCDB
                .Setup(service => service.CalcularCDBAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.True(resultado.ValorLiquido < resultado.ValorBruto);
            Assert.Equal(expectedResponse.ValorLiquido, resultado.ValorLiquido);
        }

        [Fact]
        public async Task Handle_MaximumTermInvestment_AppliesLongTermTax()
        {
            var command = new CalcularCDBCommand { ValorInicial = 1000, PrazoMeses = 1200 };
            var expectedValorBruto = 1099422640.03m;
            var expectedValorLiquido = 934510744.08m;

            var expectedResponse = new CDBCalculoResultado
            {
                ValorBruto = expectedValorBruto,
                ValorLiquido = expectedValorLiquido
            };

            _mockServicoCalculadoraCDB
                .Setup(service => service.CalcularCDBAsync(command.ValorInicial, command.PrazoMeses))
                .ReturnsAsync(expectedResponse);

            var resultado = await _handler.Handle(command, default);

            Assert.NotNull(resultado);
            Assert.Equal(expectedValorBruto, resultado.ValorBruto);
            Assert.Equal(expectedValorLiquido, resultado.ValorLiquido);
        }


        [Theory]
        [InlineData(1000, 12, 20.0)]
        
        public async Task CalcularCDBAsync_ValorLiquidoCalculadoCorretamente(decimal valorInicial, int prazoMeses, double taxaImposto)
        {
            
            decimal valorBrutoEsperado = CDBCalculator.CalcularValorBruto(valorInicial, prazoMeses);
            decimal valorLiquidoEsperado = CDBCalculator.CalcularValorLiquido(valorBrutoEsperado, valorInicial, (decimal)taxaImposto);

            // Act
            var resultado = await _servicoCalculadoraCDB.CalcularCDBAsync(valorInicial, prazoMeses);

            // Assert
            Assert.Equal(valorBrutoEsperado, resultado.ValorBruto);
            Assert.Equal(valorLiquidoEsperado, resultado.ValorLiquido);
        }
       
    }
}