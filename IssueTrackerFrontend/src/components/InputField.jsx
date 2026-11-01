import { useState } from "react";

const API_URL = "https://localhost:7043/api/tickets/UpdateTicket";

export default function InputField({ label, value, ticketId }) {
    const [inputValue, setInputValue] = useState(value);
    const [isEditing, setIsEditing] = useState(false);

    const handleChange = (e) => {
        if (!isEditing)
            setIsEditing(true);
        setInputValue(e.target.value);
    };

    const handleSaveOrCancel = async (action) => {
        if (action === "cancel") {
            setInputValue(value);
            setIsEditing(false);
            return;
        }
        const response = await fetch(`${API_URL}/${ticketId}`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ label, value: inputValue }),
        });
        if (!response.ok) {
            console.log(`response.status: ${response.status}`);
        } else {
            console.log(`Successfully updated ${label.toLowerCase()} for ticket ID: ${ticketId}`);
            setIsEditing(false);
        }
    };

    if (label === "Comment") {
        return (<>
            <input value={inputValue} onChange={(e) => handleChange(e)} />
            {isEditing && <button onClick={() => handleSaveOrCancel("save")}>Save</button>}
            {isEditing && <button onClick={() => handleSaveOrCancel("cancel")}>Cancel</button>}
        </>)
    } else {
        return (
            <div>
                <label>{label}</label>
                <input value={inputValue} onChange={(e) => handleChange(e)} />
                {isEditing && <button onClick={() => handleSaveOrCancel("save")}>Save</button>}
                {isEditing && <button onClick={() => handleSaveOrCancel("cancel")}>Cancel</button>}
            </div>
        )
    }
}