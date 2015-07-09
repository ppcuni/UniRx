﻿// original code from rx.codeplex.com
// some modified.

/* ------------------ */

// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

#if SystemReactive
namespace System.Reactive
#else
namespace UniRx
#endif
{
    /// <summary>
    /// Represents a .NET event invocation consisting of the strongly typed object that raised the event and the data that was generated by the event.
    /// </summary>
    /// <typeparam name="TSender">
    /// The type of the sender that raised the event.
    /// This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// The type of the event data generated by the event.
    /// This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.
    /// </typeparam>
    public interface IEventPattern<TSender, TEventArgs>
    {
        /// <summary>
        /// Gets the sender object that raised the event.
        /// </summary>
        TSender Sender { get; }

        /// <summary>
        /// Gets the event data that was generated by the event.
        /// </summary>
        TEventArgs EventArgs { get; }
    }

    /// <summary>
    /// Represents a .NET event invocation consisting of the weakly typed object that raised the event and the data that was generated by the event.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the event data generated by the event.</typeparam>
    public class EventPattern<TEventArgs> : EventPattern<object, TEventArgs>
    {
        /// <summary>
        /// Creates a new data representation instance of a .NET event invocation with the given sender and event data.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event data that was generated by the event.</param>
        public EventPattern(object sender, TEventArgs e)
            : base(sender, e)
        {
        }
    }

    /// <summary>
    /// Represents a .NET event invocation consisting of the strongly typed object that raised the event and the data that was generated by the event.
    /// </summary>
    /// <typeparam name="TSender">The type of the sender that raised the event.</typeparam>
    /// <typeparam name="TEventArgs">The type of the event data generated by the event.</typeparam>
    public class EventPattern<TSender, TEventArgs> : IEquatable<EventPattern<TSender, TEventArgs>>, IEventPattern<TSender, TEventArgs>
    {
        /// <summary>
        /// Creates a new data representation instance of a .NET event invocation with the given sender and event data.
        /// </summary>
        /// <param name="sender">The sender object that raised the event.</param>
        /// <param name="e">The event data that was generated by the event.</param>
        public EventPattern(TSender sender, TEventArgs e)
        {
            Sender = sender;
            EventArgs = e;
        }

        /// <summary>
        /// Gets the sender object that raised the event.
        /// </summary>
        public TSender Sender { get; private set; }

        /// <summary>
        /// Gets the event data that was generated by the event.
        /// </summary>
        public TEventArgs EventArgs { get; private set; }

        /// <summary>
        /// Determines whether the current EventPattern&lt;TSender, TEventArgs&gt; object represents the same event as a specified EventPattern&lt;TSender, TEventArgs&gt; object.
        /// </summary>
        /// <param name="other">An object to compare to the current EventPattern&lt;TSender, TEventArgs&gt; object.</param>
        /// <returns>true if both EventPattern&lt;TSender, TEventArgs&gt; objects represent the same event; otherwise, false.</returns>
        public bool Equals(EventPattern<TSender, TEventArgs> other)
        {
            if (object.ReferenceEquals(null, other))
                return false;
            if (object.ReferenceEquals(this, other))
                return true;

            return EqualityComparer<TSender>.Default.Equals(Sender, other.Sender) && EqualityComparer<TEventArgs>.Default.Equals(EventArgs, other.EventArgs);
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to the current EventPattern&lt;TSender, TEventArgs&gt;.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current EventPattern&lt;TSender, TEventArgs&gt;.</param>
        /// <returns>true if the specified System.Object is equal to the current EventPattern&lt;TSender, TEventArgs&gt;; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as EventPattern<TSender, TEventArgs>);
        }

        /// <summary>
        /// Returns the hash code for the current EventPattern&lt;TSender, TEventArgs&gt; instance.
        /// </summary>
        /// <returns>A hash code for the current EventPattern&lt;TSender, TEventArgs&gt; instance.</returns>
        public override int GetHashCode()
        {
            var x = EqualityComparer<TSender>.Default.GetHashCode(Sender);
            var y = EqualityComparer<TEventArgs>.Default.GetHashCode(EventArgs);
            return (x << 5) + (x ^ y);
        }

        /// <summary>
        /// Determines whether two specified EventPattern&lt;TSender, TEventArgs&gt; objects represent the same event.
        /// </summary>
        /// <param name="first">The first EventPattern&lt;TSender, TEventArgs&gt; to compare, or null.</param>
        /// <param name="second">The second EventPattern&lt;TSender, TEventArgs&gt; to compare, or null.</param>
        /// <returns>true if both EventPattern&lt;TSender, TEventArgs&gt; objects represent the same event; otherwise, false.</returns>
        public static bool operator ==(EventPattern<TSender, TEventArgs> first, EventPattern<TSender, TEventArgs> second)
        {
            return object.Equals(first, second);
        }

        /// <summary>
        /// Determines whether two specified EventPattern&lt;TSender, TEventArgs&gt; objects represent a different event.
        /// </summary>
        /// <param name="first">The first EventPattern&lt;TSender, TEventArgs&gt; to compare, or null.</param>
        /// <param name="second">The second EventPattern&lt;TSender, TEventArgs&gt; to compare, or null.</param>
        /// <returns>true if both EventPattern&lt;TSender, TEventArgs&gt; objects don't represent the same event; otherwise, false.</returns>
        public static bool operator !=(EventPattern<TSender, TEventArgs> first, EventPattern<TSender, TEventArgs> second)
        {
            return !object.Equals(first, second);
        }
    }
}