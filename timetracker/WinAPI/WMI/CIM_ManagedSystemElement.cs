using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinAPI.WMI
{
    /// <summary>
    /// The CIM_ManagedSystemElement class is the base class for the system
    /// element hierarchy. Any distinguishable system component is a candidate
    /// for inclusion in this class. Examples include software components, such
    /// as files; devices, such as disk drives and controllers; and physical
    /// components, such as chips and cards.
    ///
    /// Important The DMTF(Distributed Management Task Force)
    /// CIM(Common Information Model) classes are the parent classes upon
    /// which WMI classes are built. WMI currently supports only the CIM 2.x
    /// version schemas.
    /// </summary>
    public abstract class CIM_ManagedSystemElement : BaseClass
    {
        /// <summary>
        /// A short textual description of the object.
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        /// Was Caption queried
        /// </summary>
        public bool CaptionWasQueried { get; private set; }

        /// <summary>
        /// A textual description of the object.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Was Description queried
        /// </summary>
        public bool DescriptionWasQueried { get; private set; }

        /// <summary>
        /// Indicates when the object was installed. Lack of a value does not
        /// indicate that the object is not installed.
        /// </summary>
        public DateTime? InstallDate { get; private set; }

        /// <summary>
        /// Was InstallDate queried
        /// </summary>
        public bool InstallDateWasQueried { get; private set; }

        /// <summary>
        /// Label by which the object is known. When subclassed, this property
        /// can be overridden to be a key property.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Was Name queried
        /// </summary>
        public bool NameWasQueried { get; private set; }

        /// <summary>
        /// String that indicates the current status of the object.
        /// Operational and non-operational status can be defined. Operational
        /// status can include "OK", "Degraded", and "Pred Fail". "Pred Fail"
        /// indicates that an element is functioning properly, but is
        /// predicting a failure (for example, a SMART-enabled hard disk
        /// drive).
        ///
        /// Non-operational status can include "Error", "Starting", "Stopping",
        /// and "Service". "Service" can apply during disk mirror-resilvering,
        /// reloading a user permissions list, or other administrative work.
        /// Not all such work is online, but the managed element is neither
        /// "OK" nor in one of the other states.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Was Status queried
        /// </summary>
        public bool StatusWasQueried { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mbo"></param>
        public CIM_ManagedSystemElement(ManagementBaseObject mbo)
        {
            Caption = GetValueString(mbo, nameof(Caption),
                q => CaptionWasQueried = q);

            Description = GetValueString(mbo, nameof(Description),
                s => DescriptionWasQueried = s);

            InstallDate = GetValueDateTime(mbo, nameof(InstallDate),
                s => InstallDateWasQueried = s);

            Name = GetValueString(mbo, nameof(Name),
                s => NameWasQueried = s);

            Status = GetValueString(mbo, nameof(Status),
                s => StatusWasQueried = s);
        }
    }
}
