import { React, useState, useEffect } from "react";
import axios from "axios";
import "./Comments.css";

export default function Comments(props) {
  let api = process.env.REACT_APP_API_URL;
  let token = `bearer ${localStorage.getItem("token")}`;
  axios.defaults.headers.common.Authorization = token;

  let object = { content: "", projectId: "" };
  const [form, setForm] = useState(object);

  const [comments, setComments] = useState([]);
  const [userId, setUserId] = useState("");

  let projectId = props.projectId;

  const getUserId = () => {
    axios.get(`${api}/Identity/GetUserId`, { token }).then((response) => {
      setUserId(response.data);
    });
  };

  const getComments = () => {
    axios.get(`${api}/Comments/${projectId}`, { token }).then((response) => {
      setComments(response.data);
    });
  };

  const postComment = (event) => {
    event.preventDefault();

    axios.post(`${api}/Comments`, form, { token }).then((response) => {
      window.location.reload();
    });
  };

  const deleteComment = (commentId) => {
    axios.delete(`${api}/Comments/${commentId}`).then((response) => {
      window.location.reload();
    });
  };

  useEffect(() => {
    getUserId();
    getComments();
  }, []);

  return (
    <div className="comment-container">
      <h3 className="comment-container-title">Comments</h3>
      <div className="comments">
        {comments.map((comment) => {
          return (
            <div
              key={comment.id}
              className="comment flex items-start justify-start"
            >
              <div className="flex-1">
                <h3 className="comment-author">{comment.user}</h3>
                <p className="comment-body">{comment.content}</p>
                {userId === comment.createdBy ? (
                  <button
                    onClick={() => {
                      deleteComment(comment.id);
                    }}
                  >
                    Delete
                  </button>
                ) : (
                  ""
                )}
              </div>
            </div>
          );
        })}
      </div>
      <div className="comment comment-new flex items-start justify-start">
        <div className="flex-1">
          <form onSubmit={postComment} className="comment-form">
            <textarea
              placeholder="Add a comment"
              className="comment-input"
              onChange={(event) => {
                let value = event.target.value;
                setForm((previousState) => {
                  return {
                    ...previousState,
                    content: value,
                    projectId: props.projectId,
                  };
                });
              }}
            ></textarea>
            <input type="submit" className="comment-submit" value="Submit" />
          </form>
        </div>
      </div>
    </div>
  );
}
