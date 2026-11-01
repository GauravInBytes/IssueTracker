import { Link, useLoaderData } from "react-router";

const API_URL = "https://localhost:7043/api/tickets/GetAllTicketsByProjectId";

export async function ticketsLoader() {
    const response = await fetch(API_URL);

    if (!response.ok) {
        throw new Error(`Failed to fetch tickets: ${response.status}`);
    }

    const data = await response.json();
    console.log("Fetched tickets:", data);
    return data;
}

export default function TicketList() {
    const tickets = useLoaderData();

    const openTicketDetails = (ticketId) => {
        console.log(`Open details for ticket ID: ${ticketId}`);
    }

    return (
        <div className="tickets-table-wrapper">
            <h1>Tickets</h1>
            <table className="tickets-table">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Project ID</th>
                        <th>Status</th>
                        <th>Priority</th>
                        <th>Assigned To</th>
                        <th>Created By</th>
                        <th>Created At</th>
                    </tr>
                </thead>

                <tbody>
                    {tickets.map((ticket) => (
                        <tr key={ticket.ticketId}>
                            <td>
                                <Link to={`/tickets/${ticket.ticketId}`}>{ticket.title}</Link>
                            </td>
                            <td>{ticket.project.name}</td>
                            <td>{ticket.status}</td>
                            <td>{ticket.priority}</td>
                            <td>{ticket.assignedTo.fullName || "Unassigned"}</td>
                            <td>{ticket.creator.fullName}</td>
                            <td>{new Date(ticket.createdAt).toLocaleDateString()}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}