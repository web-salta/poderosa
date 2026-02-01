#!/bin/bash

# Git Authentication Fix Script for Poderosa Repository
# This script helps fix the "Authentication failed" error

set -e

echo "==================================="
echo "Git Authentication Fix Utility"
echo "==================================="
echo ""

# Check current remote URL
CURRENT_URL=$(git remote get-url origin 2>/dev/null || echo "")

if [ -z "$CURRENT_URL" ]; then
    echo "Error: No git remote 'origin' found."
    echo "Are you in a git repository?"
    exit 1
fi

echo "Current remote URL: $CURRENT_URL"
echo ""

# Detect if using HTTPS
if [[ "$CURRENT_URL" == https://* ]]; then
    echo "You are using HTTPS authentication."
    echo ""
    echo "Select an option:"
    echo "1) Switch to SSH (recommended)"
    echo "2) Continue with HTTPS (requires Personal Access Token)"
    echo "3) Cancel"
    echo ""
    read -p "Enter your choice [1-3]: " choice
    
    case $choice in
        1)
            # Switch to SSH
            SSH_URL="git@github.com:web-salta/poderosa.git"
            echo ""
            echo "Switching to SSH..."
            git remote set-url origin "$SSH_URL"
            echo "✓ Remote URL updated to: $SSH_URL"
            echo ""
            echo "Testing SSH connection..."
            if ssh -T git@github.com 2>&1 | grep -q "successfully authenticated"; then
                echo "✓ SSH authentication successful!"
                echo ""
                echo "You can now push and pull without password prompts."
            else
                echo "⚠ SSH authentication test failed."
                echo ""
                echo "You may need to set up SSH keys:"
                echo "1. Generate SSH key: ssh-keygen -t ed25519 -C \"your_email@example.com\""
                echo "2. Add to ssh-agent: ssh-add ~/.ssh/id_ed25519"
                echo "3. Add public key to GitHub: https://github.com/settings/keys"
                echo ""
                echo "After setting up SSH, try: git push origin main"
            fi
            ;;
        2)
            # Keep HTTPS, provide instructions
            echo ""
            echo "To use HTTPS, you need a Personal Access Token (PAT)."
            echo ""
            echo "Steps:"
            echo "1. Create a PAT at: https://github.com/settings/tokens"
            echo "   - Click 'Generate new token (classic)'"
            echo "   - Select 'repo' scope"
            echo "   - Generate and copy the token"
            echo ""
            echo "2. When you push, use your token as password:"
            echo "   Username: <your-github-username>"
            echo "   Password: <your-personal-access-token>"
            echo ""
            echo "3. To cache credentials:"
            echo "   git config --global credential.helper cache"
            echo ""
            echo "You can now try: git push origin main"
            ;;
        3)
            echo "Operation cancelled."
            exit 0
            ;;
        *)
            echo "Invalid choice. Operation cancelled."
            exit 1
            ;;
    esac
elif [[ "$CURRENT_URL" == git@* ]]; then
    echo "You are using SSH authentication."
    echo ""
    echo "Testing SSH connection..."
    if ssh -T git@github.com 2>&1 | grep -q "successfully authenticated"; then
        echo "✓ SSH authentication successful!"
        echo ""
        echo "Your authentication is properly configured."
    else
        echo "⚠ SSH authentication test failed."
        echo ""
        echo "You may need to set up SSH keys:"
        echo "1. Generate SSH key: ssh-keygen -t ed25519 -C \"your_email@example.com\""
        echo "2. Add to ssh-agent: ssh-add ~/.ssh/id_ed25519"
        echo "3. Add public key to GitHub: https://github.com/settings/keys"
    fi
else
    echo "Unknown remote URL format."
    echo "Please check your git configuration."
    exit 1
fi

echo ""
echo "==================================="
echo "For more help, see:"
echo "- README.md"
echo "- CONTRIBUTING.md"
echo "==================================="
