using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using PagoEfectivo.Api.Domain.Const;
using PagoEfectivo.Api.Domain.Model;
using PagoEfectivo.Api.Infra;
using Xunit;

namespace PagoEfectivo.UnitTest.FakerFixtures
{
    [CollectionDefinition(nameof(FakerCollection))]
    public class FakerCollection: ICollectionFixture<FakerFixture>
    {}
    public class FakerFixture : IDisposable
    {
        public PagoEfectivoContext GenerarDbContext()
        {
            var mockContexto = new Mock<PagoEfectivoContext>();
            mockContexto.Setup(m => m.Estado).Returns(GenerarDbSetEstado());
            mockContexto.Setup(m => m.Promocion).Returns(GenerarDbSetPromocion());
            return mockContexto.Object;
        }
        private DbSet<Estado> GenerarDbSetEstado()
        {
            var dataPrueba = GenerarDataEstado().AsQueryable();
            var estadoDbSet = new Mock<DbSet<Estado>>();

            estadoDbSet.As<IQueryable<Estado>>().Setup(m => m.Provider)
            .Returns(dataPrueba.Provider);

            estadoDbSet.As<IQueryable<Estado>>().Setup(m => m.Expression)
            .Returns(dataPrueba.Expression);

            estadoDbSet.As<IQueryable<Estado>>().Setup(m => m.ElementType)
           .Returns(dataPrueba.ElementType);

            estadoDbSet.As<IQueryable<Estado>>().Setup(m => m.GetEnumerator())
           .Returns(dataPrueba.GetEnumerator());

            estadoDbSet.As<IAsyncEnumerable<Estado>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
           .Returns(new AsyncEnumerator<Estado>(dataPrueba.GetEnumerator()));

            return estadoDbSet.Object;

        }
        private DbSet<Promocion> GenerarDbSetPromocion()
        {
            var promociones = GenerarDataPromocion().ToList();
            promociones.Add(new Promocion
            {
                Codigo = "ABCDEF",
                Email = "repetido@hotmail.com",
                Nombre = "Repetido",
                Id_Estado = EstadoConst.GENERADO,
                Id_Promocion = Guid.NewGuid()
            });
            var dataPrueba = promociones.AsQueryable();
            var promocionDbSet = new Mock<DbSet<Promocion>>();

            promocionDbSet.As<IAsyncEnumerable<Promocion>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
            .Returns(new AsyncEnumerator<Promocion>(dataPrueba.GetEnumerator()));

            promocionDbSet.As<IQueryable<Promocion>>().Setup(m => m.Provider)
            .Returns(new AsyncQueryProvider<Promocion>(dataPrueba.Provider));

            promocionDbSet.As<IQueryable<Promocion>>().Setup(m => m.Expression)
            .Returns(dataPrueba.Expression);

            promocionDbSet.As<IQueryable<Promocion>>().Setup(m => m.ElementType)
           .Returns(dataPrueba.ElementType);

            promocionDbSet.As<IQueryable<Promocion>>().Setup(m => m.GetEnumerator())
           .Returns(dataPrueba.GetEnumerator());

            promocionDbSet.Setup(d => d.Add(It.IsAny<Promocion>())).Callback<Promocion>((s) => promociones.Add(s));
            promocionDbSet.Setup(d => d.Update(It.IsAny<Promocion>())).Callback<Promocion>(e => {
                promociones[promociones.IndexOf(e)] = e;
            });

            return promocionDbSet.Object;

        }
        public void Dispose()
        {
        }

        private IEnumerable<Promocion> GenerarDataPromocion()
        {
            return new Faker<Promocion>()
            .RuleFor(o => o.Id_Promocion, f => f.Random.Guid())
            .RuleFor(o => o.Email, f => f.Person.Email)
            .RuleFor(o => o.Nombre, f => f.Person.FirstName)
            .RuleFor(o => o.Id_Estado, f => f.Random.Int(1, 2))
            .RuleFor(o => o.Codigo, f => f.Random.AlphaNumeric(6).ToUpper())
            .Generate(10);
        }
        private IEnumerable<Estado> GenerarDataEstado()
        {
            return new List<Estado>{
               new Estado{
                   Id_Estado = EstadoConst.GENERADO,
                    Descripcion = nameof(EstadoConst.GENERADO)
               },
               new Estado{
                   Id_Estado = EstadoConst.CANJEADO,
                    Descripcion = nameof(EstadoConst.CANJEADO)
               }
           };
        }
    }
    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;
        public T Current => enumerator.Current;
        public AsyncEnumerator(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }
        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(enumerator.MoveNext());
        }
    }
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(IEnumerable<T> enumerable)
     : base(enumerable)
        { }
        public AsyncEnumerable(Expression expression) : base(expression)
        {

        }
        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
        IQueryProvider IQueryable.Provider
        {
            get { return new AsyncQueryProvider<T>(this); }
        }
    }
    public class AsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal AsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new AsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new AsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Execute<TResult>(expression);
        }
    }


}