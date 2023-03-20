using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intentio.TUIO
{
    /**
     * <para>
     * The TuioListener interface provides a simple callback infrastructure which is used by the {@link TuioClient} class
     * to dispatch TUIO events to all registered instances of classes that implement the TuioListener interface defined here.
     * </para>
     * <para>
     * Any class that implements the TuioListener interface is required to implement all of the callback methods defined here.
     * The {@link TuioClient} makes use of these interface methods in order to dispatch TUIO events to all registered TuioListener implementations.
     * </para>
     * <example>
     * <code>
     * public class MyTuioListener implements TuioListener
     * ...
     * MyTuioListener listener = new MyTuioListener();
     * TuioClient client = new TuioClient();
     * client.addTuioListener(listener);
     * client.start();
     * </code>
     * </example>
     *
     * @author Martin Kaltenbrunner
     * @version 1.1.6
     */
    public interface TuioListener
    {
        /**
         * <summary>
         * This callback method is invoked by the TuioClient when a new TuioObject is added to the session.</summary>
         *
         * <param name="tobj">the TuioObject reference associated to the addTuioObject event</param>
         */
        void addTuioObject(TuioObject tobj);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when an existing TuioObject is updated during the session.</summary>
         *
         * <param name="tobj">the TuioObject reference associated to the updateTuioObject event</param>
         */
        void updateTuioObject(TuioObject tobj);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when an existing TuioObject is removed from the session.</summary>
         *
         * <param name="tobj">the TuioObject reference associated to the removeTuioObject event</param>
         */
        void removeTuioObject(TuioObject tobj);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when a new TuioCursor is added to the session.</summary>
         *
         * <param name="tcur">the TuioCursor reference associated to the addTuioCursor event</param>
         */
        void addTuioCursor(TuioCursor tcur);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when an existing TuioCursor is updated during the session.</summary>
         *
         * <param name="tcur">the TuioCursor reference associated to the updateTuioCursor event</param>
         */
        void updateTuioCursor(TuioCursor tcur);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when an existing TuioCursor is removed from the session.</summary>
         *
         * <param name="tcur">the TuioCursor reference associated to the removeTuioCursor event</param>
         */
        void removeTuioCursor(TuioCursor tcur);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when a new TuioBlob is added to the session.</summary>
         *
         * <param name="tblb">the TuioBlob reference associated to the addTuioBlob event</param>
         */
        void addTuioBlob(TuioBlob tblb);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when an existing TuioBlob is updated during the session.</summary>
         *
         * <param name="tblb">the TuioBlob reference associated to the updateTuioBlob event</param>
         */
        void updateTuioBlob(TuioBlob tblb);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient when an existing TuioBlob is removed from the session.</summary>
         *
         * <param name="tblb">the TuioBlob reference associated to the removeTuioBlob event</param>
         */
        void removeTuioBlob(TuioBlob tblb);

        /**
         * <summary>
         * This callback method is invoked by the TuioClient to mark the end of a received TUIO message bundle.</summary>
         *
         * <param name="ftime">the TuioTime associated to the current TUIO message bundle</param>
         */
        void refresh(TuioTime ftime);
    }
}
