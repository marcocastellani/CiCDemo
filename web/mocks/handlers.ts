import { http, HttpResponse } from 'msw'

const sleep = async (ms: number): Promise<void> =>
  new Promise((resolve) => setTimeout(resolve, ms))

export const handlers = [
  http.get('http://localhost:5193/api/post', async () => {
    return HttpResponse.json([
      {
        postId: 46,
        title: 'Post corto',
        content: 'bar',
        size: 1,
      },
      {
        postId: 47,
        title: 'post lungo',
        content:
          '123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 ',
        size: 2,
      },
      {
        postId: 48,
        title: 'Post strano',
        content: 'bar',
        size: 1,
      },
      {
        postId: 49,
        title: 'Post strano',
        content: 'bar dasdasdasdas',
        size: 1,
      },
    ])
  }),

  http.get('http://localhost:3000/api/doclist', async () => {
    const data: DocList = [
      { name: 'React', url: 'https://react.dev/' },
      { name: 'Vite', url: 'https://vitejs.dev/' },
      {
        name: 'React Router',
        url: 'https://reactrouter.com/en/main/start/overview',
      },
      { name: 'MSW', url: 'https://mswjs.io/' },
      { name: 'Tailwind CSS', url: 'https://tailwindcss.com/' },
    ]

    await sleep(2000)

    return HttpResponse.json(data)
  }),
]
