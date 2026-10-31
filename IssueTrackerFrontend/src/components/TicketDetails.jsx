import { Link, useLoaderData } from "react-router";

const API_URL = "https://localhost:7043/api/tickets";

export async function ticketDetailsLoader({ params }) {
  const response = await fetch(
    `${API_URL}/${params.ticketId}`
  );

  if (!response.ok) {
    throw new Error("Could not load this ticket.");
  }

  return response.json();
}

export default function TicketDetails() {
  const ticket = useLoaderData();

  return (
    <main>
      <Link to="/tickets">← Back to tickets</Link>

      <h1>{ticket.title}</h1>
      <p>{ticket.description}</p>
      <p>Status: {ticket.status}</p>
      <p>Priority: {ticket.priority}</p>
    </main>
  );
}