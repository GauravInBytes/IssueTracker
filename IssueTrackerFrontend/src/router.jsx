import { createBrowserRouter } from "react-router";

import App from "./App.jsx";
import TicketList, { ticketsLoader } from "./components/TicketList";
import TicketDetails, { ticketDetailsLoader } from "./components/TicketDetails";

export const router = createBrowserRouter([
  {
    Component: App,
    children: [
      {
        path: "/tickets",
        Component: TicketList,
        loader: ticketsLoader,
      },
      {
        path: "/tickets/:ticketId",
        Component: TicketDetails,
        loader: ticketDetailsLoader,
      },
    ],
  },
]);