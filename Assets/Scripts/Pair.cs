using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*---------------------------------------------------------------------------------------
--  SOURCE FILE:    Pair.cs
--
--  PROGRAM:        Linux Game
--
--  FUNCTIONS:
--
--  DATE:           January 20th, 2016
--
--  REVISIONS:      January 20th, 2016: Created
--                  January 21st, 2016: Refactored to use generics
--
--  DESIGNERS:      Carson Roscoe
--
--  PROGRAMMER:     Carson Roscoe
--
--  NOTES:
--  Unity usees .NET version 3.5, before Tuples were a thing. Because of that, we had
--  to make our own tuple class. I called this class Pair, since it is a key/value pair.
--
--  Pair required operator overloading for comparing these in such a way that Dictionaries
--  could successfully use it as a key to search and compare with.
---------------------------------------------------------------------------------------*/
public class Pair<T, Y> {
    //First value, the key
    public T first;
    //Second value, the value
    public Y second;

    //Create a readonly comparer for key type
    private static readonly IEqualityComparer Item1Comparer = EqualityComparer<T>.Default;
    //Create a readonly comparer for the value type
    private static readonly IEqualityComparer Item2Comparer = EqualityComparer<Y>.Default;

    /*---------------------------------------------------------------------------------------------------------------------
    -- METHOD: Constructor
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: Pair(T key
    --                 Y value)
    --
    -- NOTES:
    -- Assigns the key and value for our key/value pair type.
    ---------------------------------------------------------------------------------------------------------------------*/
    public Pair(T first, Y second) {
        this.first = first;
        this.second = second;
    }

    /*---------------------------------------------------------------------------------------------------------------------
    -- METHOD: ToString
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: string ToString(void)
    --
    -- RETURNS: void
    --
    -- NOTES:
    -- Overridden ToString method used for printing a key
    ---------------------------------------------------------------------------------------------------------------------*/
    public override string ToString() {
        return string.Format("<{0}, {1}>", first, second);
    }

    /*---------------------------------------------------------------------------------------------------------------------
    -- OPERATOR: Equality / ==
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: bool operator ==(Pair<T, Y> a, Pair<T, Y> b)
    --
    -- RETURNS: bool regarding if two pairs are equal
    --
    -- NOTES:
    -- Overloaded equality operator used for checking if two Pair objects are equal
    ---------------------------------------------------------------------------------------------------------------------*/
    public static bool operator ==(Pair<T, Y> a, Pair<T, Y> b) {
        if (IsNull(a) && !IsNull(b))
            return false;

        if (!IsNull(a) && IsNull(b))
            return false;

        if (IsNull(a) && IsNull(b))
            return true;

        return
            a.first.Equals(b.first) &&
            a.second.Equals(b.second);
    }

    /*---------------------------------------------------------------------------------------------------------------------
    -- OPERATOR: Inequality / !=
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: bool operator !=(Pair<T, Y> a, Pair<T, Y> b)
    --
    -- RETURNS: bool regarding if they are not equal
    --
    -- NOTES:
    -- Overloaded inequality operator used for checking if two Pair objects are not equal
    ---------------------------------------------------------------------------------------------------------------------*/
    public static bool operator !=(Pair<T, Y> a, Pair<T, Y> b) {
        return !(a == b);
    }

    /*---------------------------------------------------------------------------------------------------------------------
    -- METHOD: GetHashCode
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: int GetHashCode(void)
    --
    -- RETURNS: int of hashed value of pair
    --
    -- NOTES:
    -- Overridden GetHashCode method used to return a hash of the given pair object
    ---------------------------------------------------------------------------------------------------------------------*/
    public override int GetHashCode() {
        int hash = 17;
        int multiplier = 21;
        hash = hash * multiplier + first.GetHashCode();
        hash = hash * multiplier + second.GetHashCode();
        return hash;
    }

    /*---------------------------------------------------------------------------------------------------------------------
    -- METHOD: Equals
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: bool Equals(object obj)
    --
    -- RETURNS: bool over whether a pair equals another object
    --
    -- NOTES:
    -- Overridden Equals method used to return if a pair equals the object passed in
    ---------------------------------------------------------------------------------------------------------------------*/
    public override bool Equals(object obj) {
        var other = obj as Pair<T, Y>;
        if (ReferenceEquals(other, null))
            return false;
        else
            return Item1Comparer.Equals(first, other.first) &&
                   Item2Comparer.Equals(second, other.second);
    }

    /*---------------------------------------------------------------------------------------------------------------------
    -- METHOD: IsNull
    --
    -- DATE: January 20th, 2016
    --
    -- REVISIONS: N/A
    --
    -- DESIGNER: Carson Roscoe
    --
    -- PROGRAMMER: Carson Roscoe
    --
    -- INTERFACE: bool IsNull(object to check if is null)
    --
    -- RETURNS: bool over whether a object is null
    --
    -- NOTES:
    -- Returns if a object is null
    ---------------------------------------------------------------------------------------------------------------------*/
    private static bool IsNull(object obj) {
        return ReferenceEquals(obj, null);
    }
}