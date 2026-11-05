# Specification – PortfolioSite

## 1 The project in my own words

A portfolio site for employers and recruiters who want to see what kinds of projects I make.
The visitor can browse my public GitHub projects, filter them by language, open a project to read more, and go to its live demo or GitHub repo.
Delivered as a Blazor WebAssembly site on GitHub Pages, built from a public GitHub repo.

Parts: Core, Web, Workflows, Docs, Mermaid

## 2 Requirements

Main flow: an employer opens the site, sees the projects I have chosen, opens one, and goes on to its live demo or its repo on GitHub.

Core (MUST):

- List the projects from projects.json in the order of the file (with title, short description, tech and image)
- Open a project page (with the full description, what I learned, tech and links)
- Link each project to its repo on GitHub
- Show a "Live demo" button when the project has a demo address

Rules:

- A project without an image shows a placeholder
- When projects.json cannot be read, the page shows "Could not load projects"

Later / bonus (NOT NOW):

- Featured projects first, then the rest by date
- Hide a project without removing it from the file
- Fetch stars and last update from the GitHub API
- Filter by tech
- Live terminal for console applications

## 3 Flow

![Main flow](diagrams/main-flow.svg)

The user experience visiting the site ([Mermaid source](diagrams/main-flow.mmd))

## 4 Interface

## 5 Test list

## 6 Questions, assumptions and decisions
