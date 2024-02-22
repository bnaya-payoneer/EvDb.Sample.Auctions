
using EvDb.Core;

namespace EvDb.Sample.Auctions;

public record PublishedEvent(IEvDbEventPayload Payload, IEvDbEventMeta Metadata);
