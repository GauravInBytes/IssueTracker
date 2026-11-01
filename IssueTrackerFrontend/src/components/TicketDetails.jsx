import { useState } from "react";
import { Link, useLoaderData } from "react-router";

import InputField from "./InputField";

const API_URL = "https://localhost:7043/api/tickets/GetTicketById";

export async function ticketDetailsLoader({ params }) {
    const response = await fetch(
        `${API_URL}/${params.ticketId}`
    );

    if (!response.ok) {
        console.error(`Failed to fetch ticket details: ${response.status}`);
        throw new Error("Could not load this ticket.");
    }
    console.log("Fetched ticket details:", await response.clone().json());
    return response.json();
}

export default function TicketDetails() {
    const ticket = useLoaderData();

    return (
        <main>
            <Link to="/tickets">← Back to tickets</Link>
            <InputField label="Description" value={ticket.description} ticketId={ticket.ticketId} />
            <h2>Comments</h2>
            {ticket.comments.map((comment) => (
                <div key={comment.commentId}>
                    <InputField label="Comment" value={comment.body} ticketId={ticket.TicketId} />
                </div>
            ))}
        </main>
    );
}