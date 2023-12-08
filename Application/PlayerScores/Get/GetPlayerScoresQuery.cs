﻿using Application.Abstractions.Messaging;

namespace Application.PlayerScores.Get;

public sealed record GetPlayerScoresQuery(
    int Page,
    int PageSize) : IQuery<PagedList<PlayerScoreResponse>>;
