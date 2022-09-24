﻿using QueryPressure.Core.Interfaces;

namespace QueryPressure.Core;

public class QueryExecutor
{
    private readonly IExecutable _executable;
    private readonly IProfile _loadProfile;

    public QueryExecutor(IExecutable executable, IProfile loadProfile)
    {
        _executable = executable;
        _loadProfile = loadProfile;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (await _loadProfile.WhenNextCanBeExecutedAsync(cancellationToken))
        {
            await _executable.ExecuteAsync(cancellationToken);
        }
    }
}