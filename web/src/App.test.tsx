import { render, screen, waitFor } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import React from 'react'

import App from './App'

test('Logo is displayed', () => {
  render(<App />)

  expect(screen.getByText('ALT - Agile Lean Tuscany')).toBeInTheDocument()
})

test('working with msw', async () => {
  render(<App />)

  await waitFor(
    () => {
      expect(screen.getByText('Nerdism')).toBeInTheDocument()
      expect(
        screen.getByText(process.env.REACT_APP_TEXT as string),
      ).toBeInTheDocument()
    },
    { timeout: 5000 },
  )
})
