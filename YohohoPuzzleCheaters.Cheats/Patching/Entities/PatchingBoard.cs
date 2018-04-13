﻿using System;
using System.Linq;

namespace YohohoPuzzleCheaters.Cheats.Patching.Entities
{
    public class PatchingBoard : IEquatable<PatchingBoard>
    {
        readonly PatchingPiece[] pieces;

        public int Width { get; }

        public int Height { get; }

        public PatchingBoard(int width, int height)
        {
            Width = width;
            Height = height;

            pieces = new PatchingPiece[width * height];

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = PatchingPiece.Unknown;
            }
        }

        public PatchingPiece this[int x, int y]
        {
            get
            {
                return pieces[y * Width + x];
            }
            set
            {
                pieces[y * Width + x] = value;
            }
        }

        public PatchingPiece this[int index]
        {
            get
            {
                return pieces[index];
            }
            set
            {
                pieces[index] = value;
            }
        }

        public bool ContainsUnknownPieces => pieces.Any(x => x.Type == PatchingPieceType.Unknown);

        public PatchingBoard CreateCopy()
        {
            PatchingBoard board = new PatchingBoard(Width, Height);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    board[x, y] = this[x, y];
                }
            }

            return board;
        }

        public bool Equals(PatchingBoard other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Width != other.Width || Height != other.Height)
            {
                return false;
            }

            for (int i = 0; i < Width * Height; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((PatchingBoard)obj);
        }

        public override int GetHashCode()
        {
            int hash = 911 ^ Width ^ Height;

            for (int i = 0; i < Width * Height; i++)
            {
                hash ^= this[i].GetHashCode();
            }

            return hash;
        }

        public static bool operator ==(PatchingBoard b1, PatchingBoard b2) => b1.Equals(b2);

        public static bool operator !=(PatchingBoard b1, PatchingBoard b2) => !b1.Equals(b2);
    }
}
