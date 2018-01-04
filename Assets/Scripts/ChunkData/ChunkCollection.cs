using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCollection<T> {

    public ChunkCollection()
    {
        _chunkSize = (int)DEFAULT_CHUNK_SIZE;
    }
    public ChunkCollection(uint size)
    {
        _chunkSize = (int)size;
    }

    public event System.Func<IntVector2, Chunk<T>> CreateChunk;

    private const uint DEFAULT_CHUNK_SIZE = 32;

    protected readonly int _chunkSize;

    private Dictionary<IntVector2, Chunk<T>> _chunks = new Dictionary<IntVector2, Chunk<T>>();
    
    protected virtual void Initialize()
    {
        CreateChunk = chunkPosition => { return new Chunk<T>((uint)_chunkSize); };
    }
    public virtual T Get(IntVector2 worldPosition)
    {
        T obj = default(T);

        Work(worldPosition, (localPosition, chunk) => { obj = chunk[(uint)localPosition.x, (uint)localPosition.y]; });

        return obj;
    }
    public virtual void Remove(IntVector2 worldPosition)
    {
        Work(worldPosition, (localPosition, chunk) => { chunk.Remove(localPosition); });
    }
    public virtual void Add(IntVector2 worldPosition, T obj)
    {
        Work(worldPosition, (localPosition, chunk) => { chunk.Add(localPosition, obj); });
    }
    /// <summary>
    /// Safely executes work on a chunk
    /// </summary>
    /// <param name="worldPosition">Target of the work</param>
    /// <param name="action">Work to execute</param>
    protected void Work(IntVector2 worldPosition, System.Action<IntVector2, Chunk<T>> action)
    {
        IntVector2 localPosition = Utility.WorldToChunkLocalPosition(worldPosition, _chunkSize);
        IntVector2 chunkPosition = Utility.WorldToChunkPosition(worldPosition, _chunkSize);

        PollChunk(chunkPosition);

        Chunk<T> chunk = _chunks[chunkPosition];

        action(localPosition, chunk);
    }
    protected virtual void PollChunk(IntVector2 worldPosition)
    {
        IntVector2 chunkPosition = Utility.WorldToChunkPosition(worldPosition, _chunkSize);

        if (!_chunks.ContainsKey(chunkPosition))
        {
            Chunk<T> chunk = CreateChunk(chunkPosition);

            _chunks.Add(chunkPosition, chunk);
        }
    }
}