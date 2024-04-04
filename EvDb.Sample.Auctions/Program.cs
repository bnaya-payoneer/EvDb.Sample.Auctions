using EvDb.Sample.Auctions;
using System.Threading.Channels;
using Projectors = EvDb.Sample.Auctions.Projectors;
using Processors = EvDb.Sample.Auctions.Processors;
using CreateAuction = EvDb.Sample.Auctions.CommandsHandlers.CreateAuction;
using PlaceBid = EvDb.Sample.Auctions.CommandsHandlers.PlaceBid;
using CloseAuction = EvDb.Sample.Auctions.CommandsHandlers.CloseAuction;
using EvDb.Sample.Auctions.Abstractions;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                 .AddJsonOptions(o =>
                 {
                     var opt = o.JsonSerializerOptions;
                     opt.Converters.Add(new JsonStringEnumConverter());
                     opt.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddEvDbSqlServerStore();
builder.Services.AddKeyedSingleton(
    Constants.OpenAuctionsChannelKey,
    Channel.CreateUnbounded<PublishedEvent>());
builder.Services.AddSingleton<CreateAuction.IHandler, CreateAuction.Handler>();
builder.Services.AddSingleton<PlaceBid.IHandler, PlaceBid.Handler>();
builder.Services.AddSingleton<CloseAuction.IHandler, CloseAuction.Handler>();
builder.Services.AddEvDbAuctionStreamFactory();
builder.Services.AddEvDbOpenAuctionsStreamFactory();
builder.Services.AddKeyedSingleton(
    Constants.OpenAuctionsProjectionKey,
    async (sp, key) =>
    {
        var factory = sp.GetRequiredService<IEvDbOpenAuctionsStreamFactory>();
        IEvDbOpenAuctionsStream stream = await factory.GetAsync(key?.ToString() ?? throw new KeyNotFoundException());
        return stream;
    });
builder.Services.AddSingleton<Projectors.OpenAuctions.Handler>();
builder.Services.AddSingleton<Projectors.OpenAuctions.IView>(sp =>
sp.GetRequiredService<Projectors.OpenAuctions.Handler>());
builder.Services.AddSingleton<Projectors.OpenAuctions.IListener>(sp =>
sp.GetRequiredService<Projectors.OpenAuctions.Handler>());
builder.Services.AddHostedService<Projectors.OpenAuctions.HostedService>();
builder.Services.AddHostedService<Processors.AuctionCloser.HostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

