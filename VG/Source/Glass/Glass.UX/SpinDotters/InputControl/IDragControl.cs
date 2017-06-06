// (c) Norbert Huffschmid
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.

using System;

namespace SpinDotters.InputControl
{
    /// <summary>
    /// Interface for a dragging controls
    /// </summary>
    public interface IDragControl
    {
        /// <summary>
        /// Event fired when spin angle has changed
        /// </summary>
        event EventHandler SpinAngleChanged;

        /// <summary>
        /// Event fired when the dragging condition has changed
        /// </summary>
        event EventHandler DraggingChanged;


        /// <summary>
        /// Get or sets the dragging condition.
        /// </summary>
        bool Dragging { get; set; }

        /// <summary>
        /// Get or sets the current spin angle.
        /// </summary>
        double SpinAngle { get; set; }
    }
}
