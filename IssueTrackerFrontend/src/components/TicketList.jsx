import { Link, useLoaderData } from "react-router";

export async function ticketsLoader() {
  const response = await fetch("https://localhost:7043/api/tickets");

  if (!response.ok) {
    throw new Error(`Failed to fetch tickets: ${response.status}`);
  }

  const data = await response.json();
  console.log("Fetched tickets:", data);
  return data;
}

export default function TicketList() {
    const tickets = useLoaderData();
    console.log(tickets);
    const openTicketDetails = (ticketId) => {
        console.log(`Open details for ticket ID: ${ticketId}`);
    }

    return (
        <div className="tickets">
            <h1>Tickets</h1>
            {tickets && tickets.length > 0 ? (
                <ul>
                    {tickets.map(ticket => (
                        <li key={ticket.ticketId} className="ticketList">
                            <Link to={`/tickets/${ticket.ticketId}`}>{ticket.title}</Link>
                            <p>Status: {ticket.status}</p>
                            <p>Priority: {ticket.priority}</p>
                            <p>Assigned to: {ticket.assignedTo}</p>
                            <p>Created by: {ticket.createdBy}</p>
                            <p>Created at: {new Date(ticket.createdAt).toLocaleString()}</p>
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No tickets available.</p>
            )}
        </div>
    )
}